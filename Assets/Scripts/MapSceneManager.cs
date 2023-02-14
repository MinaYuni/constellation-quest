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
        MapMenu.SetActive(b);
    }

    public void setStateLearnMenu(bool b){
        LearnMenu.SetActive(b);
    }
}
