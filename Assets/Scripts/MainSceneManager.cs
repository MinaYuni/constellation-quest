using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    private GameData gameData;
	public GameData prefabGameData;
    public GameObject MainMenu;
    public GameObject LevelMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameData"))
        {
            gameData = Instantiate(prefabGameData);
            gameData.name = "GameData";
           	DontDestroyOnLoad(gameData.gameObject);
        }
        else
        {
            gameData = GameObject.Find("GameData").GetComponent<GameData>();

            
        }
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
