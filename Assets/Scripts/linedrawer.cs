using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class linedrawer : MonoBehaviour
{
    static linedrawer()
    {
        SceneView.duringSceneGui += SceneGUI;
    }
    
    private static GameObject firstSelectedSprite;
    private static GameObject secondSelectedSprite;
    private static Transform SelectedSpritesParent;
    private static bool selectingSprites;

    private static void SceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && e.shift)
        {
            Vector2 mousePos = e.mousePosition;

            Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, -ray.direction);
            
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (firstSelectedSprite == null)
                {
                    firstSelectedSprite = hit.collider.gameObject;
                }
                else
                {
                    secondSelectedSprite = hit.collider.gameObject;
                    SelectedSpritesParent = secondSelectedSprite.transform.parent;
                    string star1 = firstSelectedSprite.transform.name.Replace("Star", "");
                    string star2 = secondSelectedSprite.transform.name.Replace("Star", "");
                    string name = star1 + "-" + star2;
                    CreateLink(Math.Min(firstSelectedSprite.transform.localScale.x, secondSelectedSprite.transform.localScale.x), name);
                    firstSelectedSprite = null;
                    secondSelectedSprite = null;
                }
            }
            else
            {
                firstSelectedSprite = null;
                secondSelectedSprite = null;
            }

            e.Use();
        }
    }

    private static void CreateLink(float size, string number)
    {
        GameObject go = new GameObject("Link" + number);
        go.tag = "Link";
        go.transform.parent = SelectedSpritesParent;
        LineRenderer lineRenderer = go.AddComponent<LineRenderer>();

        float gapSize = size/100;

        Vector3 direction = (secondSelectedSprite.transform.position - firstSelectedSprite.transform.position).normalized;

        Vector3 firstSpritePosition = firstSelectedSprite.transform.position + direction * gapSize;
        Vector3 secondSpritePosition = secondSelectedSprite.transform.position - direction * gapSize;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, firstSpritePosition);
        lineRenderer.SetPosition(1, secondSpritePosition);
        
        lineRenderer.startWidth = size/400;
        lineRenderer.endWidth = size/400;
        
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.useWorldSpace = false;

    }
}
