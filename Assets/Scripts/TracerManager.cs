using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TracerManager : MonoBehaviour
{

    public GameData gameData;
    public Transform constellation; // la constellation 
    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    bool allLinksDisplayed = false; // si toutes les liens sont visibles �a veut dire que la constellation est finie 

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
    }

    // Update is called once per frame
    void Update()
    {
        allLinksDisplayed = checkAllLinkDisplayed();

        if (allLinksDisplayed)
        {
            Debug.Log("Constellation fini");
        }
    }

    bool checkAllLinkDisplayed()
    {
        int cpt = 0;

        foreach (Transform link in listObjectLinks)
        {
            if (link.gameObject.activeInHierarchy == true)
            {
                cpt++;
            }
        }

        if (cpt == listObjectLinks.Count)
        {
            gameData.ConstLearnt[constellation.name] = true;
            return true;
        }

        return false;
    }
}
