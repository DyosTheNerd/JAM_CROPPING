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
    
    private Transform[] goals;

    public static GoalManager instance; 
    
    void Start()
    {
        instance = this;
    }
    
    
    public void GenerateLevel()
    {
        Random r = new Random();
        r.InitState(123456);
        goals = new Transform[numberOfGoals];
        if (goalPrefab != null)
        {
            for (int i = 0; i < numberOfGoals; i++)
            {
                Transform newObject = Instantiate(goalPrefab);
                newObject.position = new Vector3(r.NextInt(minX,maxX), r.NextInt(minY,maxY), 0);
                SpriteRenderer newSprite = newObject.GetComponent<SpriteRenderer>();
                newSprite.color = Colors.colorList[r.NextInt(0,Colors.colorList.Length)];
                goals[i] = newObject;
            }
        }
    }

    public void EncloseArea(Vector3[] polygon)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            polygon[0],polygon[1],polygon[2]
        };

        int[] triangles = new int[]
        {
            0, 1, 2 
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        GameObject polygonObject = new GameObject("Polygon");
        MeshFilter meshFilter = polygonObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = polygonObject.AddComponent<MeshRenderer>();
        meshRenderer.material.color = Colors.colorList[1];

        meshFilter.mesh = mesh;

        // You may need to assign a material to the mesh renderer for it to be visible.

        // Adjust the position, rotation, and scale of the polygonObject as needed.
        polygonObject.transform.position = Vector3.back;
    }
}