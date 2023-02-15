using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class MapSceneManager : MonoBehaviour
{
    public GameObject buttonRetourMap; 
    public GameObject MapMenu;
    public GameObject LearnMenu;
    public GameObject menuFin;
    public TMP_Text nomConstellation;
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

    public void setBouttonRetourMap(bool b)
    {
        buttonRetourMap.SetActive(b);
    }

    public void setStateLearn(GameObject constel){
        //Debug.Log("learn menu set active?");
        LearnMenu.SetActive(true);
        Debug.Log(constel.name);

        // Transform[] allChildren = LearnMenu.GetComponentsInChildren<Transform>();

        foreach (Transform child in LearnMenu.transform){
            // Debug.Log("child name == constel name : " + child.name + " " + constel.name);
            if (child.name == constel.name)
            {
                child.gameObject.SetActive(true);

                resetConstellation(child);
            }
            else if (child.name == "NomConstellation")
            {
                child.gameObject.SetActive(true);
                nomConstellation.GetComponent<TextMeshProUGUI>().text = constel.name;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }

    public void refaireConst(TMP_Text nomConst)
    {
        Debug.Log("refaireConst");
        Debug.Log(nomConst.text.ToString());

        Transform child = LearnMenu.transform.Find(nomConst.text.ToString());
        menuFin.SetActive(false);
        resetConstellation(child);
        child.gameObject.SetActive(true);
    }

    void resetConstellation(Transform child)
    {
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
