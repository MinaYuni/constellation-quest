using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void LoadScene(string sceneName)
    {
        Debug.Log("Changed scene to " + sceneName);
        SceneManager.LoadScene(sceneName);
        // GameObjectManager.LoadScene(sceneName);
    }

}