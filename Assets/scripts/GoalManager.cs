using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private Transform goalPrefab;

    [SerializeField] private int numberOfGoals;

    private int minY = -9;
    private int maxY = 9;
    private int minX = -103;
    private int maxX = -73;
    
    private Goal[] goals;

    public static GoalManager instance; 
    
    void Start()
    {
        instance = this;
    }
    
    
    public void GenerateLevel()
    {
        Random r = new Random();
        r.InitState(123456);
        goals = new Goal[numberOfGoals];
        if (goalPrefab != null)
        {
            for (int i = 0; i < numberOfGoals; i++)
            {
                Transform newObject = Instantiate(goalPrefab);
                newObject.position = new Vector3(r.NextInt(minX,maxX), r.NextInt(minY,maxY), 0);
                SpriteRenderer newSprite = newObject.GetComponent<SpriteRenderer>();
                newSprite.color = Colors.colorList[r.NextInt(0,Colors.colorList.Length)];
                goals[i] = newObject.GetComponent<Goal>();
                newObject.parent = transform;
            }
        }
    }

    public void EncloseArea(Vector3[] polygon)
    {
        Color areaColor = _determineColor(polygon);
        AreaManager.instance.DrawArea(areaColor,polygon);
        
    }

    private Color _determineColor(Vector3[] polygon)
    {
        List<Goal> enclosedGoals = new List<Goal>();
        
        
        for (int i = 0;( goals != null) && i < goals.Length; i++)
        {
            
            if (goals[i].IsEnclosedBy(polygon))
            {
                enclosedGoals.Add(goals[i]);
            }
        }

        if (enclosedGoals.Count > 0)
        {
            
            return enclosedGoals[0].GetColor();
        }
        
        return Colors.grey;
    }
    
}