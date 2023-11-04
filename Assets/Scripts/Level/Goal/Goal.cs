using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
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
                Debug.Log(myComponents[i]._color);
                Debug.Log(itemsToScore[k].GetColor());
                
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
        float red = 0;
        float green = 0;
        float blue = 0;
        int numberOfSolved = 0;
        for (int i = 0; i < myComponents.Length; i++)
        {
            if (myComponents[i].solved)
            {
                red += myComponents[i]._color.r;
                green += myComponents[i]._color.g;
                blue += myComponents[i]._color.b;
                numberOfSolved += 1;
            }
        }

        Debug.Log("Number of Goals: "+ myComponents.Length);
        
        Debug.Log("Number of solved:" + numberOfSolved);
        
        if (numberOfSolved > 0)
        {
            red = red / numberOfSolved;
            green = green / numberOfSolved;
            blue = blue / numberOfSolved;
        }
        else
        {
            blue = red = green = Colors.grey.b;
        }


        Color result = new Color();
        result.r = red;
        result.g = green;
        result.b = blue;
        result.a = Colors.grey.a;

        return result;
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