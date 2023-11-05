using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI tmpfield;
    
    void Start()
    {
        LevelScoreManager.instance.OnScoreUpdate += UpdateScore;
        
    }

    void UpdateScore(int score)
    {
        Debug.Log("ui score");
        if (tmpfield)
        {
            tmpfield.text = "Score: " + score;
        }
    }
    
}
