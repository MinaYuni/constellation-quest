using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TMPro;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Tobii.Gaming;
using System.Runtime.CompilerServices;

public class ChangeColor : MonoBehaviour
{
    public SpriteRenderer starSprite;
    public Transform star;
    //public RectTransform canvas;
    //public GameObject gazePoint;

    //public TMP_Text xGazeText;
    //public TMP_Text yGazeText;
    //public TMP_Text isGazeHoveringText;
    //public TMP_Text posStarText;

    //private float _pauseTimer;
    //private Outline _xOutline;
    //private Outline _yOutline;

    private float xStar;
    private float yStar;
    private float radiusStar = 50;

    bool isHovering = false;
    bool isLinked = false;

    float timeToWait = 1.0f;
    float timeLeft = 1000.0f;

    Color colorStarSelected = Color.red; // rouge si sélectionné 
    Color colorStarLinked = Color.yellow; // jaune si lié 

    void Start()
    {
        //(float, float) posStar = getPositionStar(star);

        //xStar = posStar.Item1;
        //yStar = posStar.Item2;

        //posStarText.text = "( " + xStar + " ; " + yStar + " )";
        //Debug.Log(star.name + " : ( " + xStar + " ; " + yStar + " )");

        //_xOutline = xCoord.GetComponent<Outline>();
        //_yOutline = yCoord.GetComponent<Outline>();
    }

    void Update()
    {
        //updateEyeTracking();

        updateStarColor();
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

    void updateStarColor()
    {
        if (starSprite.color == colorStarLinked)
        {
            isLinked = true;
        }
        if (isHovering)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0)
        {
            if (starSprite.color != colorStarSelected) // rouge = étoile sélectionnée (si je n'ai pas déjà le cas)
            {
                starSprite.color = colorStarSelected;
                timeLeft = 1000.0f;
            }
            else if (isLinked) // jaune = étoile liée 
            {
                starSprite.color = colorStarLinked;
                timeLeft = 1000.0f;
            }
            else // sinon blanche 
            {
                starSprite.color = Color.white;
                timeLeft = 1000.0f;
            }

        }
    }

    bool isInCircle(float x, float y, float centerX, float centerY, float radius)
    {
        return (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) <= radius * radius;
    }

    /*
    (float, float) getPositionStar(Transform star)
    {
        starSprite = GetComponent<SpriteRenderer>();

        Vector2 sizeCanvas = canvas.sizeDelta;

        Vector3 starScale = star.localScale;
        Vector3 starPositionLocal = star.position;
        Vector3 starPositionWorld = star.InverseTransformVector(starPositionLocal);
        float starX = starPositionWorld.x * starScale.x;
        float starY = starPositionWorld.y * starScale.y;

        float xStarPosition = starX + (sizeCanvas.x / 2.0f);
        float yStarPosition = starY + (sizeCanvas.y / 2.0f);

        return (xStarPosition, yStarPosition);
    }
    

    void updateEyeTracking()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        Vector2 gazePosition = gazePoint.Screen;

        //Debug.Log("( " + gazePosition.x + " ; " + gazePosition.y + " )");

        if (isInCircle(gazePosition.x, gazePosition.y, xStar, yStar, radiusStar))
        {
            //starSprite.color = Color.blue;
            //isGazeHoveringText.text = "YES";
            //Debug.Log("YES");

            if (isHovering == false)
            {
                timeLeft = timeToWait;
                isHovering = true;
            }
        }
        else
        {
            //isGazeHoveringText.text = "NO";
            isHovering = false;
        }
    }
    */
}

