using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SingleScoreDisplay : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;


    public void setValues(int rank, int score, string name)
    {
        rankText.text = rank.ToString();
        scoreText.text = score.ToString();
        nameText.text = name;
    }
}
