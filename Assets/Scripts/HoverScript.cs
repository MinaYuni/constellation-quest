using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript : MonoBehaviour
{

    public GameObject go;
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;

    //This stores the GameObject s original color
    Color m_OriginalColor;

    //Get the GameObject mesh renderer to access the GameObject material and color
    // MeshRenderer m_Renderer;

    void Start()
    {
        // m_Renderer = GetComponent<MeshRenderer>();
        // m_OriginalColor = m_Renderer.material.color;
    }

    void OnMouseOver()
    {
        // GameObject go = GameObject.Find("MyGameObjectName");
        // Change the color of the GameObject to red when the mouse is over GameObject
        // m_Renderer.material.color = m_MouseOverColor;
        Debug.Log("Mouse is over GameObject " + go.name);
    }

    void OnMouseExit()
    {
        // GameObject go = GameObject.Find("MyGameObjectName");

        // Reset the color of the GameObject back to normal
        // m_Renderer.material.color = m_OriginalColor;
        Debug.Log("Mouse is not over GameObject " + go.name);
    }
}