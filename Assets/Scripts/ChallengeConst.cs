using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using TMPro;
using UnityEngine.SceneManagement;

public class ChallengeConst : MonoBehaviour
{

    private GameData gameData;

    public List<string> constellations = new List<string>();

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

        foreach(var learnt in gameData.ConstLearnt){
            if(learnt.Value == true){
                constellations.Add(learnt.Key);
            }
        }

    }

    // Update is called once per frame
    void Update(){
        
    }
}
