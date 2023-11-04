using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class ItemManager : MonoBehaviour
{
    [FormerlySerializedAs("goalPrefab")] [SerializeField] private Transform itemPrefab;

    [FormerlySerializedAs("numberOfGoals")] [SerializeField] private int numberOfItems;

    private int minY = -9;
    private int maxY = 9;
    private int minX = -103;
    private int maxX = -73;
    
    private Item[] items;

    public static ItemManager instance; 
    
    void Start()
    {
        instance = this;
    }
    
    
    public void GenerateLevel()
    {
        Random r = new Random();
        r.InitState(123456);
        items = new Item[numberOfItems];
        if (itemPrefab != null)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                Transform newObject = Instantiate(itemPrefab);
                newObject.position = new Vector3(r.NextInt(minX,maxX), r.NextInt(minY,maxY), 0);
                SpriteRenderer newSprite = newObject.GetComponent<SpriteRenderer>();
                newSprite.color = Colors.colorList[r.NextInt(0,Colors.colorList.Length)];
                items[i] = newObject.GetComponent<Item>();
                newObject.parent = transform;
            }
        }
    }

    public void EncloseArea(Vector3[] polygon)
    {
        Color areaColor = _scoreItemsAndDetermineColor(polygon);
        AreaManager.instance.DrawArea(areaColor,polygon);
        
    }

    private Color _scoreItemsAndDetermineColor(Vector3[] polygon)
    {
        List<Item> enclosedGoals = new List<Item>();
        
        
        for (int i = 0;( items != null) && i < items.Length; i++)
        {
            
            if (items[i].IsEnclosedBy(polygon))
            {
                enclosedGoals.Add(items[i]);
            }
        }

        if (enclosedGoals.Count > 0)
        {

            return GoalManager.instance.ScoreItemsAndDetermineColor(enclosedGoals);
        }
        
        return Colors.grey;
    }
    
}