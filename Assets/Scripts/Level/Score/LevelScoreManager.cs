using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LevelScoreManager: MonoBehaviour
{
    public static LevelScoreManager instance;

    private int score = 0;
    
    public delegate void ScoreUpdate(int newScore);

    public event ScoreUpdate OnScoreUpdate;


    private void OnEnable()
    {
        instance = this;
    }

    private void Start()
    {
        GoalManager.instance.OnGoalSolved += ScoreGoal;
    }

    private void ScoreGoal(Goal g)
    {
        Debug.Log("score updated");
        score += 1;

        _notifyScoreUpdate();
    }

    private void _notifyScoreUpdate()
    {
        OnScoreUpdate?.Invoke(score);
    }
    
    
    
}
