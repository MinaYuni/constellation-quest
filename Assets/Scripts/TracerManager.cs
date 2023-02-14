using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using TMPro;
using UnityEngine.SceneManagement;

public class TracerManager : MonoBehaviour
{

    private GameData gameData;
    public Transform constellation; // la constellation 
    public GameObject menuFin; // menu de fin 
    //public TMP_Text nomConstellation;
    public TMP_Text textMenuFin;

    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    bool allLinksDisplayed = false; // si toutes les liens sont visibles �a veut dire que la constellation est finie 
    Color colorStarSelected = Color.red; // rouge si s�lectionn� 
    Color colorStarLinked = Color.yellow; // jaune si li� 


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

        foreach (Transform child in constellation)
        {
            if (child.gameObject.tag == "Link")
            {
                listObjectLinks.Add(child);
            }
        }

        //nomConstellation.GetComponent<TextMeshProUGUI>().text = constellation.name; 
    }

    // Update is called once per frame
    void Update()
    {
        allLinksDisplayed = checkAllLinkDisplayed();

        if (allLinksDisplayed)
        {
            System.Threading.Thread.Sleep(500);
            constellation.gameObject.SetActive(false);
            menuFin.SetActive(true);
            textMenuFin.GetComponent<TextMeshProUGUI>().text = "Bravo !\nVous avez réussi à tracer la constellation " + constellation.name + " !!!";
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
            gameData.ConstLearnt[constellation.name] = true;
            // Debug.Log(constellation.name + " " + gameData.ConstLearnt[constellation.name]);
            unlockNextLevel();
            
            return true;
        }

        return false;
    }

    public void unlockNextLevel(){
        int curr = gameData.currLevel;
        bool unlock = true;

        // Debug.Log("curr : " +curr);
        // Debug.Log("game data currLevel : " +gameData.currLevel);
        // Debug.Log("gameData levels: " + gameData.levels);
        // Debug.Log("gameData levels length: " + gameData.levels.Count);

        List<string> levelToCheck = gameData.levels[curr];

        foreach(var v in levelToCheck){
            if(!gameData.ConstLearnt[v]){
                unlock = false;
                break;
            }
        }

        if(unlock){
            gameData.levelsUnlocked["Level"+ curr+2] = true;
            gameData.currLevel ++;
        }
    }

    void changeLinkColor(LineRenderer link, Color couleur, float alpha)
    {
        var gradient = new Gradient();

        gradient.mode = GradientMode.Blend;

        var gradientColorKeys = new GradientColorKey[2]
        {
            new GradientColorKey(couleur, .5f),
            new GradientColorKey(couleur, .5f)
        };

        var alphaKeys = new GradientAlphaKey[2]
        {
            new GradientAlphaKey(alpha, .5f),
            new GradientAlphaKey(alpha, .5f)
        };

        gradient.SetKeys(gradientColorKeys, alphaKeys);

        link.colorGradient = gradient;
    }
}
