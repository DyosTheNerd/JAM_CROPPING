using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static GoalManager instance;

    public Goal[] goals;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        _setupGoals(132456);

    }

    public Color ScoreItemsAndDetermineColor(List<Item>  itemsToScore)
    {
        
        return itemsToScore[0].GetColor();
    }

    private void _setupGoals(int seed)
    {
        
    }
    
}
