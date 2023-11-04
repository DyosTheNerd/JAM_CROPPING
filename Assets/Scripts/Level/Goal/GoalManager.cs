
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class GoalManager : MonoBehaviour
{
    public static GoalManager instance;

    public Goal[] goals;

    private Random myRandom;

    private int queueSize = 5;

    public bool initialized = false;
    
    public delegate void GoalSolvedEvent(Goal goal);

    public event GoalSolvedEvent OnGoalSolved;
    
    void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        _setupGoals();
    }

    public Color ScoreItemsAndDetermineColor(List<Item>  itemsToScore)
    {
        Goal solvedGoal = goals[0];

        for (int i = 0; i < goals.Length - 1; i++)
        {
            goals[i] = goals[i + 1];
        }

        goals[goals.Length] = getNewGoal();

        solvedGoal.SolveWith(itemsToScore);

        if (OnGoalSolved != null)
        {
            OnGoalSolved.Invoke(solvedGoal);    
        }
        
        return itemsToScore[0].GetColor();
    }

    public Goal GetFutureGoal(int range)
    {
        return goals[range];
    }
    
    private void _setupGoals()
    {
        LevelManager lm = LevelManager.instance;

        if (lm == null)
        {
            Debug.Log("lm is null");
        }
        
        myRandom = lm.GoalsRandom;
        goals = new Goal[queueSize];

        for (int i = 0; i < queueSize; i++)
        {
            Goal g = getNewGoal();
            goals[i] = g;
        }
        
        if (OnGoalSolved != null)
        {
            OnGoalSolved.Invoke(null);    
        }

        initialized = true;
    }

    private Goal getNewGoal()
    {
        return new Goal();
    }
    
}
