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
    //public TMP_Text nomConstellation;
    public TMP_Text textmenuNext;

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
