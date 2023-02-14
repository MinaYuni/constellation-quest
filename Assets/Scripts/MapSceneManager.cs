using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public GameObject MapMenu;
    public GameObject LearnMenu;
    // public GameObject constellation;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void setStateMapMenu(bool b)
    {
        //Debug.Log("in mapscenemanager called set map menu");
        MapMenu.SetActive(b);
    }

    public void setStateLearnMenu(bool b)
    {
        //Debug.Log("called set learn menu");
        LearnMenu.SetActive(b);
    }

    public void setStateLearn(GameObject constel){
        // Debug.Log("learn menu set active?");
        LearnMenu.SetActive(true);

        // Transform[] allChildren = LearnMenu.GetComponentsInChildren<Transform>();

        foreach (Transform child in LearnMenu.transform){
            //Debug.Log(child.name);
            if(child.name == constel.name || child.name == "NomConstellation")
            {
                // Debug.Log("child name == constel name : " + child.name + " " + constel.name);
                child.gameObject.SetActive(true);

                // reset la constellation 
                foreach (Transform c in child)
                {
                    if (c.gameObject.tag == "Link")
                    {
                        c.GetComponent<LineRenderer>().material.color = Color.white;
                        c.gameObject.SetActive(false);
                    }
                    if (c.gameObject.tag == "Star")
                    {
                        c.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }


    // public void enableConst(string const){
    //     LearnMenu.setActive(true);
        // Transform[] allChildren = LearnMenu.GetComponentsInChildren<Transform>();
        // foreach (Transform child in allChildren){
        //     if(child.name == const){
        //         child.gameObject.SetActive(true);
        //     }
        // }
    // }

    // public void stateLearnMenu(string const, bool b){
    //     LearnMenu.SetActive(b);
    //     // GameObject constellation = LearnMenu.transform.Find(const);
    //     Transform[] allChildren = LearnMenu.GetComponentsInChildren<Transform>();
    //     foreach (Transform child in allChildren){
    //         if(child.name == const){
    //             child.gameObject.SetActive(true);
    //         }
    //         // else{
    //         //     child.gameObject.SetActive(false);
    //         // }
    //     }
    // }
}
