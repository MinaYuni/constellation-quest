using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public GameObject MapMenu;
    public GameObject LearnMenu;
    // public GameObject constellation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStateMapMenu(bool b){
        Debug.Log("in mapscenemanager called set map menu");
        MapMenu.SetActive(b);
    }

    public void setStateLearnMenu(bool b){
        Debug.Log("called set learn menu");

        LearnMenu.SetActive(b);
    }

    public void setStateLearn(GameObject constel){
        LearnMenu.SetActive(true);
        // Debug.Log("learn menu set active?");

        // Transform[] allChildren = LearnMenu.GetComponentsInChildren<Transform>();
        foreach (Transform child in LearnMenu.transform){
            Debug.Log(child.name);
            if(child.name == constel.name || child.name == "NomConstellation" || child.name == "RetourMap"){
                // Debug.Log("child name == constel name : " + child.name + " " + constel.name);
                child.gameObject.SetActive(true);
                foreach (Transform link in child){
                    if (link.gameObject.tag == "Link"){
                        link.gameObject.SetActive(false);
                    }
                    if (link.gameObject.tag == "Link"){
                        link.gameObject.SetActive(false);
                    }

                }
            }
            else{
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
