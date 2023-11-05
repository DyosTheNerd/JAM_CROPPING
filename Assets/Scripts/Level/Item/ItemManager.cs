using System.Collections.Generic;
using Game;
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

    private Random myRandom;
    
    void Start()
    {
        Camera mainCamera = Camera.main; // Replace with your camera reference if needed

        if (mainCamera != null)
        {
            // Get the camera's world space bounds
            float cameraHeight = 2.0f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;
            Vector3 cameraCenter = mainCamera.transform.position;
            Bounds cameraBounds = new Bounds(cameraCenter, new Vector3(cameraWidth, cameraHeight, 0));

            minY = (int) cameraBounds.min.y;
            minX = (int) cameraBounds.min.x;
            maxY = (int) cameraBounds.max.y;
            maxX = (int) cameraBounds.max.x;
        }
        else
        {
            Debug.LogWarning("Camera not found.");
        }
        
        
        instance = this;

        LineManager.Instance.OnEnclose += OnEncloseVertices; 
        
        GenerateLevel();
    }
    
    
    public void GenerateLevel()
    {
        initializeRandom();
        items = new Item[numberOfItems];
        if (itemPrefab != null)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                Transform newObject = Instantiate(itemPrefab);
                newObject.position = new Vector3(myRandom.NextInt(minX,maxX),myRandom.NextInt(minY,maxY), 0);
                SpriteRenderer newSprite = newObject.GetComponent<SpriteRenderer>();
                newSprite.color = Colors.colorList[myRandom.NextInt(0,Colors.colorList.Length)];
                items[i] = newObject.GetComponent<Item>();
                items[i].SetColor(newSprite.color);
                newObject.parent = transform;
            }
        }
    }

    private void initializeRandom()
    {
        myRandom = LevelManager.instance.ItemsRandom;
    }
    
    
    private void OnEncloseVertices(IntersectionArgs args)
    {
        if (args.points.Length < 7)
        {
            Vector3[] okCase = new Vector3[args.points.Length];
            for (int i = 0; i < args.points.Length; i++) 
            {
                okCase[i] = new Vector3(args.points[i].position.x, args.points[i].position.y);
            }
            EncloseArea(okCase);
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