using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using TMPro;
using UnityEngine.SceneManagement;

public class TracerChallenge : MonoBehaviour
{

    private GameData gameData;
    public Transform constellation; // la constellation 
    public GameObject menuNext; // menu de fin 
    public TMP_Text textmenuNext;

    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    bool allLinksDisplayed = false; // si toutes les liens sont visibles �a veut dire que la constellation est finie 
    Color colorStarSelected = Color.red; // rouge si s�lectionn� 
    Color colorStarLinked = Color.yellow; // jaune si li� 

    float timerConst = 0.0f;
    float constTimeMax = 0.0f;
    //string toReLearn;

    void Start()
    {
        GameObject gameDataGO = GameObject.Find("GameData");
		if (gameDataGO == null)
			SceneManager.LoadScene("TitleScene");
		else
		{
			gameData = gameDataGO.GetComponent<GameData>();
        }

        foreach (Transform child in constellation)
        {
            if (child.gameObject.tag == "Link")
            {
                listObjectLinks.Add(child);
            }
        }

        constTimeMax = gameData.ConstTimeLimit[constellation.name];
        //Debug.Log(constTimeMax);
    }

    void Update()
    {
        timerConst += Time.deltaTime;

        // retirer de la liste des challenges si on a dépassé le temps max pour la constellation concernée 
        if (timerConst >= constTimeMax)
        {
            gameData.ConstTimeLearnt[constellation.name] = 0;
            gameData.ConstForChallenge.Remove(constellation.name);
        }

        allLinksDisplayed = checkAllLinkDisplayed();

        if (allLinksDisplayed)
        {
            System.Threading.Thread.Sleep(500);
            constellation.gameObject.SetActive(false);
            menuNext.SetActive(true);
            textmenuNext.GetComponent<TextMeshProUGUI>().text = "Bravo !\nVous avez réussi à tracer la constellation " + constellation.name + " !!!";
        }
    }

    bool checkAllLinkDisplayed()
    {
        int cpt = 0;

        foreach (Transform link in listObjectLinks)
        {
            if (link.gameObject.activeInHierarchy == true && link.GetComponent<LineRenderer>().material.color == colorStarLinked)
            {
                cpt++;
            }
        }

        if (cpt == listObjectLinks.Count)
        {
            return true;
        }

        return false;
    }
}
