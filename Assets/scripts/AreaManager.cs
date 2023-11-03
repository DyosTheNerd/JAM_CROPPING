using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance; 
    
    void Start()
    {
        instance = this;
    }

}
