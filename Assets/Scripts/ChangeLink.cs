using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ChangeLink : MonoBehaviour
{
    public LineRenderer link;
    Color colorStarSelected = Color.red; // rouge si sélectionné 
    Color colorStarLinked = Color.yellow; // jaune si lié 
    Color colorHelpLinks = new Color(1, 1, 1, 0.5f); // lien grisé 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log("Clicked");
        link = GetComponent<LineRenderer>();

        if (link.material.color == Color.white) 
        {
            changeColor(link, colorStarLinked, 0.5f);
        }
        else {
            changeColor(link, colorStarSelected, 1.0f);
        }


    }

    void changeColor(LineRenderer link, Color couleur, float alpha)
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
