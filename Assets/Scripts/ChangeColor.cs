using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer circle;
    bool isHovering = false;
    bool isLinked = false;
    public float timeToWait = 1.0f;
    float timeLeft = 1000.0f;

    void Start()
    {
        circle = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (circle.color == Color.red)
        {
            isLinked = true;
        }
        if (isHovering)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0)
        {
            if (circle.color != Color.yellow)
            {
                circle.color = Color.yellow;
                timeLeft = 1000.0f;
            }
            else if (isLinked)
            {
                circle.color = Color.red;
                timeLeft = 1000.0f;
            }
            else 
            {
                circle.color = Color.white;
                timeLeft = 1000.0f;
            }

        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("Clicked");
    }

    void OnMouseOver()
    {
        if (isHovering == false)
        {
            timeLeft = timeToWait;
            isHovering = true;
        }
    }

    void OnMouseExit()
    {
        isHovering = false;
    }
}
