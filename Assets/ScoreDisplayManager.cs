using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Testing Component Loading");
        GetComponent<ScoreManager>().buttonTestScore();
        Debug.Log("Component Loaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
