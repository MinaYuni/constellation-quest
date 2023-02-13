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

    Color colorStarSelected = Color.red; // rouge si s�lectionn� 
    Color colorStarLinked = Color.yellow; // jaune si li� 

    void Start()
    {
        circle = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (circle.color == colorStarLinked)
        {
            isLinked = true;
        }
        if (isHovering)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0)
        {
            if (circle.color != colorStarSelected) // rouge = �toile s�lectionn�e (si je n'ai pas d�j� le cas)
            {
                circle.color = colorStarSelected;
                timeLeft = 1000.0f;
            }
            else if (isLinked) // jaune = �toile li�e 
            {
                circle.color = colorStarLinked;
                timeLeft = 1000.0f;
            }
            else // sinon blanche 
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
