using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using TMPro;

public class TracerManager : MonoBehaviour
{

    public GameData gameData;
    public Transform constellation; // la constellation 
    public GameObject menuFin; // menu de fin 
    public TMP_Text nomConstellation;
    public TMP_Text textMenuFin;

    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    bool allLinksDisplayed = false; // si toutes les liens sont visibles �a veut dire que la constellation est finie 
    Color colorStarSelected = Color.red; // rouge si s�lectionn� 
    Color colorStarLinked = Color.yellow; // jaune si li� 


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in constellation)
        {
            if (child.gameObject.tag == "Link")
            {
                listObjectLinks.Add(child);
            }
        }

        nomConstellation.GetComponent<TextMeshProUGUI>().text = constellation.name; 
    }

    // Update is called once per frame
    void Update()
    {
        allLinksDisplayed = checkAllLinkDisplayed();

        if (allLinksDisplayed)
        {
            System.Threading.Thread.Sleep(500);
            menuFin.SetActive(true);
            textMenuFin.GetComponent<TextMeshProUGUI>().text = "Bravo !\nVous avez réussi à tracer la constellation " + constellation.name + " !!!";
        }
    }

    bool checkAllLinkDisplayed()
    {
        int cpt = 0;

        foreach (Transform link in listObjectLinks)
        {
            if (link.gameObject.activeInHierarchy == true && link.GetComponent<SpriteRenderer>().color == colorStarLinked)
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
            int currLevel = gameData.currLevel;
            bool unlock = true;

            List<string> levelToCheck = gameData.levels[currLevel];

            foreach(var v in levelToCheck){
                if(!gameData.ConstLearnt[v]){
                    unlock = false;
                    break;
                }
            }

            if(unlock){
                gameData.levelsUnlocked["Level"+ currLevel+2] = true;
                gameData.currLevel ++;
            }
        }

}
