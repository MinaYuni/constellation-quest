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
    public Transform constellation; // la cosntellation 

    Color colorStarSelected = Color.red; // rouge si sélectionné 
    Color colorLinked = Color.yellow; // jaune si lié 
    Color colorHelpLinks = new Color(1, 1, 1, 0.5f); // lien grisé 

    List<Transform> listObjectStars = new List<Transform>(); // liste des étoiles qui composent la constellation 
    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    List<string> listIdStars = new List<string>(); // liste des noms des étoiles 
    List<(Transform, string, string)> listIdLinks = new List<(Transform, string, string)>(); // (nom lien, nom étoile avant, nom étoile après)  
    List<Transform> linkedStars = new List<Transform>(); // liste des étoiles qui a été liées 

    Transform selectedStar1; // première étoile sélectionnée
    Transform selectedStar2; // deuxième étoile sélectionnée
    bool isStarSelected;
    float timeToWait = 5.0f;
    float timeLeft = 0.0f;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        //Debug.Log("Scene actuelle : " + scene.name);

        // récupérer toutes les étoiles et liens de la constellation 
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
                timeLeft = 0.0f;
            }
            else if (selectedStar1 != null || selectedStar2 == null)
            {
                timeLeft += Time.deltaTime;
            }
            else if (selectedStar1 == null || selectedStar2 != null)
            {
                timeLeft += Time.deltaTime;
            }

            //Debug.Log(timeLeft.ToString());

            activateAdaptativeHelp();         
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
            // si l'étoile a été sélectionnée, alors mettre à jour selectedStar1 et selectedStar2
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

            // si l'étoile n'a été pas sélectionnée ou elle est déjà liée, alors reset selectedStar1 et selectedStar2
            else if (star.GetComponent<SpriteRenderer>().color == Color.white || star.GetComponent<SpriteRenderer>().color == colorLinked)
            {
                if (selectedStar1 != null && selectedStar2 != null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                        timeLeft = 0.0f;
                    }
                    else if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
                        timeLeft = 0.0f;
                    }
                }
                else if (selectedStar1 != null && selectedStar2 == null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                        timeLeft = 0.0f;
                    }
                }
                else if (selectedStar1 == null && selectedStar2 != null)
                {
                    if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
                        timeLeft = 0.0f;
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
            //Debug.Log("aucune étoile sélectionnée");
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
        if (timeLeft >= timeToWait)
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

}
