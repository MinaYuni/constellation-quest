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

    public GameObject constellationsMenu;
    public GameObject startButton;
    public List<string> constellations = new List<string>();
    public List<string> solved = new List<string>();
    public int total;

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
            Debug.Log("in start challengeConst const:" + learnt.Key);
            if(learnt.Value == true){
                constellations.Add(learnt.Key);
            }
        }

        total = constellations.Count;

    }

    // Update is called once per frame
    void Update(){
        
    }

    public void startChallenge(){
        Debug.Log("here im in");
        if(constellations.Count != 0){
            Debug.Log("in challConst startChall");

        
            string cons = constellations[0];

            foreach (Transform child in constellationsMenu.transform){
                Debug.Log(child.name);
                if(child.name == cons || child.name == "NomConstellation"){
                    child.gameObject.SetActive(true);
                    foreach (Transform link in child){
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
        startButton.SetActive(false);

    }

    public void nextConstellation(){
        if(constellations.Count != 0){
            return;
        }
        string name = constellations[0];
        Debug.Log("name of prev const: " + name);
        solved.Add(name);
        constellations.Remove(name);

            if(constellations.Count != 0){
            Debug.Log("in nextConstell");

        
            string cons = constellations[0];

            foreach (Transform child in constellationsMenu.transform){
                Debug.Log(child.name);
                if(child.name == cons || child.name == "NomConstellation"){
                    child.gameObject.SetActive(true);
                    foreach (Transform link in child){
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



    }
    
}
