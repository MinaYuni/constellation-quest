using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UpdateMap : MonoBehaviour
{

    private GameData gameData;
    
    // Start is called before the first frame update
    void Start()
    {

        GameObject gameDataGO = GameObject.Find("GameData");
		if (gameDataGO == null)
			SceneManager.LoadScene("TitleScene");
		else
		{
			gameData = gameDataGO.GetComponent<GameData>();
        }

        // Debug.Log("in start of updateMap, gameData.currLevel: " + gameData.currLevel);
        // Debug.Log("in start of updateMap, gameData.levels length: " + gameData.levels.Count);

        foreach(var unlocked in gameData.levelsUnlocked){
            setLevel(unlocked.Key, unlocked.Value);
            // Debug.Log($"{unlocked.Key}: {unlocked.Value}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("in update of updateMap, gameData.currLevel: " + gameData.currLevel);
        // Debug.Log("in update");

        foreach(var unlocked in gameData.levelsUnlocked){
            // Debug.Log("in foreach in update");
            // Debug.Log($"{unlocked.Key}: {unlocked.Value}");

            setLevel(unlocked.Key, unlocked.Value);
        }
        
    }

    public void setLevel(string levelToDisable, bool activate){
        GameObject level = GameObject.Find(levelToDisable);
        if(level){
            // Debug.Log(levelToDisable);
            // Debug.Log(level.name);

            // level.transform.Find();
            Button[] buttons = level.transform.GetComponentsInChildren<Button>();
            foreach(var i in buttons){
                i.interactable = activate;
            }
        }

    }
}
