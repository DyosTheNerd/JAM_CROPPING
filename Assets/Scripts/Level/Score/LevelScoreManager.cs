using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LevelScoreManager: MonoBehaviour
{
    public static LevelScoreManager instance;

    private int score = 0;
    
    private void Start()
    {
        GoalManager.instance.OnGoalSolved += ScoreGoal;
    }

    private void ScoreGoal(Goal g)
    {
        score += 1;
    }
    
}
