using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static GoalManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public Color ScoreItemsAndDetermineColor(List<Item>  itemsToScore)
    {
        return itemsToScore[0].GetColor();
    } 
}
