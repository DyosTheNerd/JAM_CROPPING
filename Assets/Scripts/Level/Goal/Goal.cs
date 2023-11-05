using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    private GoalComponent[] myComponents;

    private bool isSolved = false;


    public Color GetColor(int i)
    {
        if (i < myComponents.Length)
        {
            return myComponents[i]._color;
        }

        return Colors.grey;
    }

    public Color SolveWith(List<Item> itemsToScore)
    {
        for (int i = 0; i < myComponents.Length; i++)
        {
            for (int k = 0; k < itemsToScore.Count; k++)
            {
                if (!itemsToScore[k].isUsedForScore())
                {
                    if (myComponents[i]._color.Equals(itemsToScore[k].GetColor()))
                    {
                        myComponents[i].solved = true;
                        itemsToScore[k].setUsedForScore(true);
                        break;
                    }
                }
            }
        }

        return getSolutionColor();
    }

    private Color getSolutionColor()
    {

        List<Color> toMix = new List<Color>();

        for (int i = 0; i < myComponents.Length; i++)
        {
            if (myComponents[i].solved)
            {
                toMix.Add(myComponents[i]._color);
                
            }
        }

       

        return Colors.getResultingColor(toMix.ToArray());
    }

    public void SetColors(Color[] targetColors)
    {
        myComponents = new GoalComponent[targetColors.Length];

        for (int i = 0; i < targetColors.Length; i++)
        {
            myComponents[i] = new GoalComponent(targetColors[i]);
        }
    }
}