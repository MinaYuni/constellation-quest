using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMap : MonoBehaviour
{

    public GameData gameData;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(var unlocked in gameData.levelsUnlocked){
            setLevel(unlocked.Key, unlocked.Value);
            // Debug.Log($"{unlocked.Key}: {unlocked.Value}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("in update");

        foreach(var unlocked in gameData.levelsUnlocked){
            setLevel(unlocked.Key, unlocked.Value);
            // Debug.Log($"{unlocked.Key}: {unlocked.Value}");
        }
        
    }

    public void setLevel(string levelToDisable, bool activate){
        GameObject level = GameObject.Find(levelToDisable);

        // level.transform.Find();
        Button[] buttons = level.transform.GetComponentsInChildren<Button>();
        foreach(var i in buttons){
            i.interactable = activate;
        }

    }
}
