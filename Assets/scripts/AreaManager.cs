using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    [SerializeField] private GameObject areaPrefab;

    [SerializeField] private GameObject areaContainer;

    [SerializeField] private List<GameObject> areaContainers;
     
    void Start()
    {
        instance = this;

        areaContainers = new List<GameObject>();
    }


    public void     DrawArea(Color color, Vector3[] polygon)
    {
        GameObject container = Instantiate(areaContainer);
        
        GameObject aSquare = _getSquare( polygon[0], polygon[1], polygon[2]);

        container.GetComponent<AreaContainer>().AddSubarea(aSquare);
        
        if (polygon.Length == 6)
        {
            GameObject aSquare2 = _getSquare( polygon[3], polygon[4], polygon[5]);
            container.GetComponent<AreaContainer>().AddSubarea(aSquare2);
            
        }

        AddContainer(container);
    }


    private GameObject _getSquare(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        GameObject newArea = Instantiate(areaPrefab);

        Vector3 newScale = point3 - point1;

        Vector3 position = new Vector3(point1.x + newScale.x / 2, point1.y + newScale.y / 2, 0);
        
        newArea.transform.localScale = newScale;

        newArea.transform.position = position;


        return newArea;
    }

    private void AddContainer(GameObject container)
    {
        areaContainers.Add(container);
    }
    
    
    
}
