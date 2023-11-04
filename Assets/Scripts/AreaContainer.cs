using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaContainer : MonoBehaviour
{


    public void AddSubarea(GameObject subarea)
    {
        subarea.transform.SetParent(transform);
    }
    
}
