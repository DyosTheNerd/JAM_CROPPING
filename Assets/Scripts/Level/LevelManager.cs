using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private Random goalsRandom;

    private Random itemsRandom;

    public static LevelManager Instance => instance;

    public Random GoalsRandom => goalsRandom;

    public Random ItemsRandom => itemsRandom;

    private Random levelMasterRandom;
    
    private void OnEnable()
    {
        instance = this;
        initializeRandoms(123);
    }
    
    
    void Start()
    {
      
    }

    void initializeRandoms(uint seed)
    {
        levelMasterRandom = new Random(12345);
        goalsRandom = new Random(levelMasterRandom.NextUInt());
        itemsRandom = new Random(levelMasterRandom.NextUInt());
    }
    
}
