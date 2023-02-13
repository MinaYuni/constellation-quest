using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TracerManager : MonoBehaviour
{
    public Transform constellation; // la cosntellation 
    public GameObject menuFin; // menu de fin 

    List<Transform> listObjectLinks = new List<Transform>(); // liste des liens qui composent la constellation 
    bool allLinksDisplayed = false; // si toutes les liens sont visibles ça veut dire que la constellation est finie 
    Color colorStarSelected = Color.red;
    Color colorStarLinked = Color.yellow;


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
            System.Threading.Thread.Sleep(1000);
            menuFin.SetActive(true);
            //Debug.Log("Constellation fini");
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
            return true;
        }

        return false;
    }
}
