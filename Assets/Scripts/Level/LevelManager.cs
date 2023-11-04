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
        initializeRandoms(SeedSingleton.getInstance().seed);
    }


    void initializeRandoms(uint seed)
    {
        Debug.Log("seed is" + seed);
        levelMasterRandom = new Random(seed);
        goalsRandom = new Random(levelMasterRandom.NextUInt());
        itemsRandom = new Random(levelMasterRandom.NextUInt());
    }
}