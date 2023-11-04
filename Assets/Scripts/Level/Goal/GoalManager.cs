
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

        goals[^1] = getNewGoal(0);

        Color outcomeColor = solvedGoal.SolveWith(itemsToScore);

        if (OnGoalSolved != null)
        {
            OnGoalSolved.Invoke(solvedGoal);    
        }
        
        return outcomeColor;
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
            Goal g = getNewGoal(0);
            goals[i] = g;
        }
        
        if (OnGoalSolved != null)
        {
            OnGoalSolved.Invoke(null);    
        }

        initialized = true;
    }

    private Goal getNewGoal(int pTargetNumber)
    {
        int numberOfTargets = pTargetNumber;
        if (pTargetNumber == null || pTargetNumber == 0)
        {
            numberOfTargets = myRandom.NextInt(1, 4);    
        }
        
        Goal newGoal = new Goal();

        Color[] targetColors = new Color[numberOfTargets];

        for (int i = 0; i < numberOfTargets; i++)
        {
            targetColors[i] = Colors.colorList[myRandom.NextInt(0, Colors.colorList.Length)];
        }


        newGoal.SetColors(targetColors);

        return newGoal;
    }
    
}
