using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Newtonsoft.Json;
using System.Security.Cryptography;

public class ShowLink : MonoBehaviour
{
    public Transform constellation; // la cosntellation 

    Color colorStarSelected = Color.red; // rouge si sélectionné 
    Color colorStarLinked = Color.yellow; // jaune si lié 
    Color colorHelpLinks = new Color(1, 1, 1, 0.5f); // lien grisé 

    List<Transform> listObjectStars = new List<Transform>(); // liste des étoiles qui composent la constellation 
    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    List<string> listIdStars = new List<string>(); // liste des noms des étoiles 
    List<(Transform, string, string)> listIdLinks = new List<(Transform, string, string)>(); // (nom lien, nom étoile avant, nom étoile après)  
    List<Transform> linkedStars = new List<Transform>(); // liste des étoiles qui a été liées 

    Transform selectedStar1; // première étoile sélectionnée
    Transform selectedStar2; // deuxième étoile sélectionnée

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

        //Debug.Log(listIdStars[0].ToString());
        //Debug.Log(listIdLinks[0].ToString()); 
        foreach ((Transform, string, string) link in listIdLinks)
        {
            Debug.Log("link1 : " + link.Item1.name);
            Debug.Log("link2 : " + link.Item2);
            Debug.Log("link3 : " + link.Item3);
        }
    }

    void Update()
    {
        selectStars();

        activateHelpLinks();

        if (selectedStar1 != null && selectedStar2 != null)
        {
            activateLink();
        }
    }

    void selectStars()
    {
        foreach (Transform star in listObjectStars)
        {
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

            else if (star.GetComponent<SpriteRenderer>().color == Color.white || star.GetComponent<SpriteRenderer>().color == colorStarLinked)
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

        /*if (selectedStar1 != null)
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
        }*/
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
                Debug.Log("link : " + link.Item1.name);
                //link.Item1.GetComponent<SpriteRenderer>().color = colorStarLinked;
                changeLinkColor(link.Item1.GetComponent<LineRenderer>(), colorStarLinked, 1.0f);         
                link.Item1.gameObject.SetActive(true);

                selectedStar1.GetComponent<SpriteRenderer>().color = colorStarLinked;
                selectedStar2.GetComponent<SpriteRenderer>().color = colorStarLinked;

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
                    //link.Item1.GetComponent<SpriteRenderer>().color = colorHelpLinks; 
                    changeLinkColor(link.Item1.GetComponent<LineRenderer>(), colorHelpLinks, 0.5f);
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
                    //link.Item1.GetComponent<SpriteRenderer>().color = colorHelpLinks;
                    changeLinkColor(link.Item1.GetComponent<LineRenderer>(), colorHelpLinks, 0.5f);
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
                }
            }
        }
    }

    void changeLinkColor(LineRenderer link, Color couleur, float alpha)
    {
        var gradient = new Gradient();

        gradient.mode = GradientMode.Blend;

        var gradientColorKeys = new GradientColorKey[2]
        {
            new GradientColorKey(couleur, .5f),
            new GradientColorKey(couleur, .5f)
        };

        var alphaKeys = new GradientAlphaKey[2]
        {
            new GradientAlphaKey(alpha, .5f),
            new GradientAlphaKey(alpha, .5f)
        };

        gradient.SetKeys(gradientColorKeys, alphaKeys);

        link.colorGradient = gradient;
    }
}
