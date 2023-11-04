using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaContainer : MonoBehaviour
{


    public void AddSubarea(GameObject subarea)
    {
        subarea.transform.SetParent(transform);
    }

    public void SetColor(Color c)
    {

        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = c;
        }
    }
    
}
