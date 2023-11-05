using System;

using UnityEngine;

public class Item : MonoBehaviour
{
    public bool usedForScore = false;

    private Color myColor;

    private SpriteRenderer myRenderer;
    
    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    public void setUsedForScore(bool isUsed)
    {
        usedForScore = isUsed;
    }

    public bool isUsedForScore()
    {
        return usedForScore;
    }

    public void SetColor(Color c)
    {
        myColor = c;
    }

    public bool enclosed = false;
    
    public bool isEnclosed()
    {
        return enclosed;
    }

    public void SetEnclosed(bool newValue)
    {
        enclosed = newValue;


        myRenderer.color = new Color(myRenderer.color.r, myRenderer.color.g, myRenderer.color.b, 0);
    }
    
    
    public Color GetColor()
    {
        return myColor;
    }

    public bool IsEnclosedBy(Vector3[] polygon)
    {
        float thisX = transform.position.x;
        float thisY = transform.position.y;

        float minX = Math.Min(polygon[0].x, polygon[2].x);
        float maxX = Math.Max(polygon[0].x, polygon[2].x);
        
        float minY = Math.Min(polygon[0].y, polygon[2].y);
        float maxY = Math.Max(polygon[0].y, polygon[2].y);
        
        
        if (thisX >= minX && thisX <= maxX && thisY >= minY && thisY <= maxY)
        {
            return true;
        }

        if (polygon.Length == 6)
        {
             minX = Math.Min(polygon[3].x, polygon[5].x);
             maxX = Math.Max(polygon[3].x, polygon[5].x);
        
             minY = Math.Min(polygon[3].y, polygon[5].y);
             maxY = Math.Max(polygon[3].y, polygon[5].y);
             
             if (thisX >= minX && thisX <= maxX && thisY >= minY && thisY <= maxY)
             {
                 return true;
             }
        }

        return false;

    }
}
