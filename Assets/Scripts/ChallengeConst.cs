using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = System.Random;


public class ChallengeConst : MonoBehaviour
{
    private GameData gameData;

    public GameObject constellationsMenu;
    public GameObject startButton;
    public GameObject menuNext;
    public GameObject menuFin;
    public GameObject menuRien; 

    public TMP_Text nomConstellation;
    public TMP_Text counter;
    public List<string> constellations = new List<string>();
    public List<string> solved = new List<string>();

    public int total;

    void Start()
    {
        GameObject gameDataGO = GameObject.Find("GameData");

		if (gameDataGO == null)
        {
			SceneManager.LoadScene("TitleScene");
        }
        else
		{
			gameData = gameDataGO.GetComponent<GameData>();
        }

        foreach(var constDispo in gameData.ConstForChallenge)
        {
            constellations.Add(constDispo);
        }

        constellations = shuffleList(constellations);

        total = constellations.Count;

        if (total == 0)
        {
            setMenuRien(true);
        }
    }

    void Update(){
        if(constellations.Count == 0){
            endChallenge();
        }
    }

    public void setMenuRien(bool b)
    {
        menuRien.SetActive(b);
    }

    public void endChallenge(){
        menuFin.SetActive(true);        
    }

    public void startChallenge(){
        //Debug.Log("here im in");

        if(constellations.Count != 0){
            //Debug.Log("in challConst startChall");
                    
            string cons = constellations[0];

            foreach (Transform child in constellationsMenu.transform){
                //Debug.Log(child.name);

                if(child.name == cons){
                    child.gameObject.SetActive(true);

                    resetConstellation(child);

                    /*
                    foreach (Transform link in child){
                        if (link.gameObject.tag == "Link"){
                            link.gameObject.SetActive(false);
                        }
                    }
                    */
                }
                else if (child.name == "NomConstellation")
                {
                    child.gameObject.SetActive(true);
                    nomConstellation.GetComponent<TextMeshProUGUI>().text = cons;
                }
                else if (child.name == "Counter")
                {
                    child.gameObject.SetActive(true);
                    counter.GetComponent<TextMeshProUGUI>().text = "Constellation : 0 / " + total;
                }
                else{
                    child.gameObject.SetActive(false);
                }
            }
        }

        startButton.SetActive(false);

    }

    public void nextConstellation(){
        // if(constellations.Count == 0){
        //     return;
        // }

        string name = constellations[0];
        solved.Add(name);
        constellations.Remove(name);

        //Debug.Log("name of prev const: " + name);

        if (constellations.Count != 0){
            //Debug.Log("in nextConstell");

            string cons = constellations[0];

            foreach (Transform child in constellationsMenu.transform){
                //Debug.Log(child.name);

                if(child.name == cons){
                    child.gameObject.SetActive(true);

                    resetConstellation(child);

                    /*
                    foreach (Transform link in child){
                        if (link.gameObject.tag == "Link"){
                            link.GetComponent<LineRenderer>().material.color = Color.white;
                            link.gameObject.SetActive(false);
                        }
                    }
                    */
                }
                else if (child.name == "NomConstellation")
                {
                    child.gameObject.SetActive(true);
                    nomConstellation.GetComponent<TextMeshProUGUI>().text = cons;
                }
                else if (child.name == "Counter")
                {
                    child.gameObject.SetActive(true);
                    counter.GetComponent<TextMeshProUGUI>().text = "Constellation : " + solved.Count + " / " + total;
                }
                else{
                    child.gameObject.SetActive(false);
                }
            }
        }
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

    List<string> shuffleList(List<string> values)
    {
        Random rand = new Random();
        List<string> shuffled = values.OrderBy(_ => rand.Next()).ToList();

        return shuffled;
    }
}
