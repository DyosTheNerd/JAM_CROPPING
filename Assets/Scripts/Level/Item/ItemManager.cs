using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

public class ItemManager : MonoBehaviour
{
    [FormerlySerializedAs("goalPrefab")] [SerializeField] private Transform itemPrefab;

    [FormerlySerializedAs("numberOfGoals")] [SerializeField] private int numberOfItems;

    public float minY = -9;
    public float maxY = 9;
    public float minX = -103;
    public float maxX = -73;

    private int cellsX = 12;
    private int cellsY = 9;
    
    private Item[] items;

    public static ItemManager instance;

    [SerializeField] private Transform insideArea;
    
    
    
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

            minY = insideArea.position.y - insideArea.lossyScale.y/2f;
            minX = insideArea.position.x - insideArea.lossyScale.x/2f;
            maxY = insideArea.position.y + insideArea.lossyScale.y/2f;
            maxX = insideArea.position.x + insideArea.lossyScale.x/2f;
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

        isTaken = new bool[cellsX * cellsY];
        
        items = new Item[numberOfItems];
        if (itemPrefab != null)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                Transform newObject = Instantiate(itemPrefab);
                newObject.position = getNextItemPosition();
                SpriteRenderer newSprite = newObject.GetComponent<SpriteRenderer>();
                newSprite.color = Colors.colorList[myRandom.NextInt(0,Colors.colorList.Length)];
                items[i] = newObject.GetComponent<Item>();
                items[i].SetColor(newSprite.color);
                newObject.parent = transform;
            }
        }
    }

    private bool[] isTaken;

    private float padding =2f;
    private Vector3 getNextItemPosition()
    {
        int x = -10;
        int y = -10;
        for (int i = 0; i < 3; i++)
        {
            x = myRandom.NextInt(0, cellsX);
            y = myRandom.NextInt(0, cellsY);
            if (!isTaken[x + cellsX * y])
            {
                isTaken[x + cellsX * y] = true;

                break;
                
            }
        }
        return   new Vector3(x* ((maxX - minX) - padding*2f) / (cellsX*1f) +minX +padding + myRandom.NextFloat(-0.125f,+0.125f),y* ((maxX - minX) - padding*2f) / (cellsX*1f)+minY+padding+ myRandom.NextFloat(-0.125f,+0.125f), 0);
    }
    
    private void initializeRandom()
    {
        myRandom = LevelManager.instance.ItemsRandom;
    }
    
    
    private void OnEncloseVertices(IntersectionArgs args)
    {
        if (args.points.Length < 5)
        {
            bool hasBeenUsed = false;
            
            Vector3[] okCase = new Vector3[args.points.Length];
            for (int i = 0; i < args.points.Length; i++)
            {
                hasBeenUsed = hasBeenUsed || args.points[i].wasUsed;
                okCase[i] = new Vector3(args.points[i].position.x, args.points[i].position.y);
                
            }

            if (!hasBeenUsed)
            {
                for (int i = 0; i < args.points.Length; i++)
                {
                    args.points[i].wasUsed = true;
                }
                EncloseArea(okCase);    
            }
            
            
        }
    }
    
    public void EncloseArea(Vector3[] polygon)
    {
        Color areaColor = _scoreItemsAndDetermineColor(polygon);
        AreaManager.instance.DrawArea(areaColor,polygon);
        CheckEndGame();
    }

    private Color _scoreItemsAndDetermineColor(Vector3[] polygon)
    {
        List<Item> enclosedItems = new List<Item>();
        
        for (int i = 0;( items != null) && i < items.Length; i++)
        {
            
            if (items[i].IsEnclosedBy(polygon) && !items[i].isEnclosed())
            {
                enclosedItems.Add(items[i]);
                items[i].SetEnclosed(true);
            }
        }
        return GoalManager.instance.ScoreItemsAndDetermineColor(enclosedItems);
    }

    private void CheckEndGame()
    {
        bool allSolved = true;
        
        for (int i = 0; i < numberOfItems; i++)
        {
            allSolved = allSolved && items[i].isEnclosed();
        }

        if (allSolved)
        {
            LevelManager.instance.triggerLevelEnd();
        }
        
    }
    
    
}