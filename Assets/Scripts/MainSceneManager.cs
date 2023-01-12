using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject LevelMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setStateMainMenu(bool b){
        MainMenu.SetActive(b);
    }

    public void setStateLevelMenu(bool b){
        LevelMenu.SetActive(b);
    }
}
