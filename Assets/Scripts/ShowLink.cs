using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ShowLink : MonoBehaviour
{
    private Scene scene;
    private GameData gameData;
    public Transform constellation; // la cosntellation 

    Color colorStarSelected = Color.red; // rouge si s�lectionn� 
    Color colorLinked = Color.yellow; // jaune si li� 
    Color colorHelpLinks = new Color(1, 1, 1, 0.5f); // lien gris� 

    List<Transform> listObjectStars = new List<Transform>(); // liste des �toiles qui composent la constellation 
    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    List<string> listIdStars = new List<string>(); // liste des noms des �toiles 
    List<(Transform, string, string)> listIdLinks = new List<(Transform, string, string)>(); // (nom lien, nom �toile avant, nom �toile apr�s)  
    List<Transform> linkedStars = new List<Transform>(); // liste des �toiles qui a �t� li�es 

    Transform selectedStar1; // premi�re �toile s�lectionn�e
    Transform selectedStar2; // deuxi�me �toile s�lectionn�e
    bool isStarSelected;

    float timeToWaitForHelp = 5.0f;
    float timerHelp = 0.0f;

    int nbAide = 0;
    float nbMaxAide = 0;
    bool aide = false;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        //Debug.Log("Scene actuelle : " + scene.name);

        GameObject gameDataGO = GameObject.Find("GameData");
        if (gameDataGO == null)
            SceneManager.LoadScene("TitleScene");
        else
        {
            gameData = gameDataGO.GetComponent<GameData>();
        }

        // r�cup�rer toutes les �toiles et liens de la constellation 
        foreach (Transform child in constellation)
        {
            if (child.gameObject.tag == "Star")
            {
                listObjectStars.Add(child);
                listIdStars.Add(child.name);
            }
            else if (child.gameObject.tag == "Link")
            {
                listObjectLinks.Add(child);
                string[] linkConnections = child.name.Replace("Link", "").Split('-');
                listIdLinks.Add((child, "Star"+linkConnections[0], "Star"+linkConnections[1]));
            }
        }

        nbMaxAide = listObjectStars.Count / 3.0f;

        //Debug.Log(listIdStars[0].ToString());
        //Debug.Log(listIdLinks[0].ToString()); 
        /*
         * foreach ((Transform, string, string) link in listIdLinks)
        {
            Debug.Log("link1 : " + link.Item1.name);
            Debug.Log("link2 : " + link.Item2);
            Debug.Log("link3 : " + link.Item3);
        }
        */
    }

    void Update()
    {
        selectStars();

        if (scene.name == "Adaptatif")
        {
            if (selectedStar1 == null && selectedStar2 == null)
            {
                //Debug.Log("reset timer");
                timerHelp = 0.0f;
            }
            else if (selectedStar1 != null && selectedStar2 == null)
            {
                timerHelp += Time.deltaTime;
            }
            else if (selectedStar1 == null && selectedStar2 != null)
            {
                timerHelp += Time.deltaTime;
            }

            //Debug.Log(timeLeft.ToString());

            activateAdaptativeHelp();

            //Debug.Log(nbAide);

            // retirer de la liste des challenges si on a d�pass� le nombre d'aide seuil pour la constellation concern�e 
            if (nbAide > nbMaxAide)
            {
                gameData.ConstTimeLearnt[constellation.name] = 0;
                gameData.ConstForChallenge.Remove(constellation.name);
            }
        }
        else
        {
            activateHelpLinks();
        }        

        if (selectedStar1 != null && selectedStar2 != null)
        {
            activateLink();
        }
    }

    void selectStars()
    {
        foreach (Transform star in listObjectStars)
        {
            // si l'�toile a �t� s�lectionn�e, alors mettre � jour selectedStar1 et selectedStar2
            if (star.GetComponent<SpriteRenderer>().color == colorStarSelected)
            {
                if (selectedStar1 == null && selectedStar2 == null)
                {
                    selectedStar1 = star;
                }
                else if (selectedStar1 == null && selectedStar2 != null)
                {
                    if (star.name != selectedStar2.name)
                    {
                        selectedStar1 = star;
                    }
                }
                else if (selectedStar1 != null && selectedStar2 == null)
                {
                    if (star.name != selectedStar1.name)
                    {
                        selectedStar2 = star;
                    }
                }
            }

            // si l'�toile n'a �t� pas s�lectionn�e ou elle est d�j� li�e, alors reset selectedStar1 et selectedStar2
            else if (star.GetComponent<SpriteRenderer>().color == Color.white || star.GetComponent<SpriteRenderer>().color == colorLinked)
            {
                if (selectedStar1 != null && selectedStar2 != null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                        timerHelp = 0.0f;
                    }
                    else if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
                        timerHelp = 0.0f;
                    }
                }
                else if (selectedStar1 != null && selectedStar2 == null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                        timerHelp = 0.0f;
                    }
                }
                else if (selectedStar1 == null && selectedStar2 != null)
                {
                    if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
                        timerHelp = 0.0f;
                    }
                }
            }
        }

        /*
        if (selectedStar1 != null)
        {
            Debug.Log("selectedStar1 : " + selectedStar1.name);
        }
        if (selectedStar2 != null)
        {
            Debug.Log("selectedStar2 : " + selectedStar2.name);
        }
        if (selectedStar1 == null && selectedStar2 == null)
        {
            //Debug.Log("aucune �toile s�lectionn�e");
        }
        */
    }

    void activateLink()
    {
        foreach ((Transform, string, string) link in listIdLinks)
        {
            //Debug.Log("--------------");
            //Debug.Log("selectedStar1 : " + selectedStar1.name);
            //Debug.Log("selectedStar2 : " + selectedStar2.name);
            //Debug.Log("link2 : " + link.Item2);
            //Debug.Log("link3 : " + link.Item3);

            if ((selectedStar1.name == link.Item2 && selectedStar2.name == link.Item3) || (selectedStar1.name == link.Item3 && selectedStar2.name == link.Item2))
            {
                //Debug.Log("link : " + link.Item1.name);

                link.Item1.GetComponent<LineRenderer>().material.color = colorLinked;
                link.Item1.gameObject.SetActive(true);

                selectedStar1.GetComponent<SpriteRenderer>().color = colorLinked;
                selectedStar2.GetComponent<SpriteRenderer>().color = colorLinked;

                foreach ((Transform, string, string) lien in listIdLinks)
                {
                    if ((selectedStar1.name == lien.Item2 || selectedStar2.name == lien.Item2) && lien.Item1.GetComponent<LineRenderer>().material.color == Color.white)
                    {
                        lien.Item1.gameObject.SetActive(false);
                    }
                }

                selectedStar1 = null; 
                selectedStar2 = null;

                break; 
            }
        }
    }

    void activateHelpLinks()
    {
        if (selectedStar1 != null && selectedStar2 == null)
        {
            foreach ((Transform, string, string) link in listIdLinks)
            {
                if ((selectedStar1.name == link.Item2 || selectedStar1.name == link.Item3) && link.Item1.gameObject.activeInHierarchy == false)
                {
                    link.Item1.GetComponent<LineRenderer>().material.color = colorHelpLinks;
                    link.Item1.gameObject.SetActive(true);
                }
            }
        }

        if (selectedStar1 == null && selectedStar2 != null)
        {
            foreach ((Transform, string, string) link in listIdLinks)
            {
                if ((selectedStar2.name == link.Item2 || selectedStar2.name == link.Item3) && link.Item1.gameObject.activeInHierarchy == false)
                {
                    link.Item1.GetComponent<LineRenderer>().material.color = colorHelpLinks;
                    link.Item1.gameObject.SetActive(true);
                }
            }
        }

        if (selectedStar1 == null && selectedStar2 == null)
        {
            foreach ((Transform, string, string) link in listIdLinks)
            {
                if (link.Item1.gameObject.activeInHierarchy == true && link.Item1.GetComponent<LineRenderer>().material.color == colorHelpLinks)
                {
                    link.Item1.gameObject.SetActive(false);
                    link.Item1.GetComponent<LineRenderer>().material.color = Color.white;
                }
            }
        }
    }

    void activateAdaptativeHelp()
    {
        if (timerHelp >= timeToWaitForHelp)
        {
            aide = true;

            if (selectedStar1 != null && selectedStar2 == null)
            {
                foreach ((Transform, string, string) link in listIdLinks)
                {
                    if ((selectedStar1.name == link.Item2 || selectedStar1.name == link.Item3) && link.Item1.gameObject.activeInHierarchy == false)
                    {
                        link.Item1.GetComponent<LineRenderer>().material.color = colorHelpLinks;
                        link.Item1.gameObject.SetActive(true);
                    }
                }
            }

            if (selectedStar1 == null && selectedStar2 != null)
            {
                foreach ((Transform, string, string) link in listIdLinks)
                {
                    if ((selectedStar2.name == link.Item2 || selectedStar2.name == link.Item3) && link.Item1.gameObject.activeInHierarchy == false)
                    {
                        link.Item1.GetComponent<LineRenderer>().material.color = colorHelpLinks;
                        link.Item1.gameObject.SetActive(true);
                    }
                }
            }
        }            

        if (selectedStar1 == null && selectedStar2 == null)
        {
            foreach ((Transform, string, string) link in listIdLinks)
            {
                if (link.Item1.gameObject.activeInHierarchy == true && link.Item1.GetComponent<LineRenderer>().material.color == colorHelpLinks)
                {
                    link.Item1.gameObject.SetActive(false);
                    link.Item1.GetComponent<LineRenderer>().material.color = Color.white;
                }
            }

            if (aide)
            {
                nbAide++;
                aide = false;
            }
        }
    }

}
