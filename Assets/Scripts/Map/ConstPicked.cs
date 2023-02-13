using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstPicked : MonoBehaviour
{
    public GameObject selectedConst;
    // public GameObject Tracing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(worldPos3d.x, worldPos3d.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.down);

            if (hit.collider != null)
            {
                selectedConst = hit.collider.gameObject;
                Debug.Log("Mouse Click Detected on " + selectedConst);

                // SceneManager.LoadScene(sceneName);
            }
        }


    } 
    
    // private void tracingMode(string sceneName)
    // {
    //     Vector3 worldPos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     Vector2 mousePos = new Vector2(worldPos3d.x, worldPos3d.y);
    //     RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.down);
    //     
    //     Debug.Log("Mouse Click Detected");
    //     SceneManager.LoadScene(sceneName);
    // }
    // public void LoadScene(string sceneName)
    // {
    //     SceneManager.LoadScene(sceneName);
    //     // GameObjectManager.LoadScene(sceneName);
    // }

}