using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;


public class ShowLink : MonoBehaviour
{
    public Transform constellation; // la cosntellation 
    List<Transform> listObjectStars = new List<Transform>(); // liste des étoiles qui composent la constellation 
    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    List<string> listIdStars = new List<string>(); // liste des noms des étoiles 
    List<(Transform, string, string)> listIdLinks = new List<(Transform, string, string)>(); // (nom lien, nom étoile avant, nom étoile après)  
    List<Transform> linkedStars = new List<Transform>(); // liste des étoiles qui a été liées 
    Transform selectedStar1; // première étoile sélectionnée
    Transform selectedStar2; // deuxième étoile sélectionnée
    
    // Start is called before the first frame update
    void Start()
    {
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

        //Debug.Log(listIdStars.ToString());
        //Debug.Log(listIdLinks.ToString()); 
    }

    // Update is called once per frame
    void Update()
    {
        selectStars();

        if (selectedStar1 != null && selectedStar2 != null)
        {
            activateLink();
        }
    }

    void selectStars()
    {
        foreach (Transform star in listObjectStars)
        {
            if (star.GetComponent<SpriteRenderer>().color == Color.yellow)
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

            else if (star.GetComponent<SpriteRenderer>().color == Color.white)
            {
                if (selectedStar1 != null && selectedStar2 != null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                    }
                    else if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
                    }
                }
                else if (selectedStar1 != null && selectedStar2 == null)
                {
                    if (star.name == selectedStar1.name)
                    {
                        selectedStar1 = null;
                    }
                }
                else if (selectedStar1 == null && selectedStar2 != null)
                {
                    if (star.name == selectedStar2.name)
                    {
                        selectedStar2 = null;
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
            Debug.Log("aucune étoile sélectionnée");
        }
        */
    }

    void activateLink()
    {
        foreach ((Transform, string, string) link in listIdLinks)
        {
            if ((selectedStar1.name == link.Item2 && selectedStar2.name == link.Item3) || (selectedStar1.name == link.Item3 && selectedStar2.name == link.Item2))
            {
                //Debug.Log("link : " + link.Item1.name);
                link.Item1.gameObject.SetActive(true);

                selectedStar1.GetComponent<SpriteRenderer>().color = Color.red;
                selectedStar2.GetComponent<SpriteRenderer>().color = Color.red;

                selectedStar1 = null; 
                selectedStar2 = null;

                break;
            }
        }
    }
}
