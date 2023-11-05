using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance { get; private set; }

    private List<LinePoints> _lines = new List<LinePoints>();
    private List<PathGraphVertex> _vertices = new List<PathGraphVertex>();

    [Header("References")]
    [SerializeField] private Transform lineContainer;
    [SerializeField] private Line linePrefab;
    [SerializeField] private Transform CornerPrefab;

    [Header("Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private float colliderEnableDelay = 0.1f;

    public delegate void EncloseEvent(IntersectionArgs data);

    public event EncloseEvent OnEnclose;

    public PathGraphVertex lastVertex => _vertices[^1]; 
    public LinePoints LastLinePoint => _lines[_lines.Count - 1];
    //public Line LastLine => _lines[_lines.Count - 1];

    Vector2 intersectionTest = new Vector2();
    Vector2 remTest = new Vector2();
    List<Vector2> drawTest = new List<Vector2>();

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        foreach (LinePoints lp in _lines)
        {
            Gizmos.color = lp.isIntersection ? Color.red : Color.green;
            Gizmos.DrawCube(lp.end, new Vector3(1, 1, 0));

            Gizmos.color = new Color(1, 1, 1, 0.25f);
            Gizmos.DrawLine(lp.start, lp.end);

            if (lp.isIntersection)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lp.end, lp.intersectedLine.start);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(lp.end, lp.intersectedLine.end);
            }


            if (intersectionTest != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(intersectionTest, 1.2f);
            }

            if( remTest != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(remTest, 1.2f);
            }

            foreach (Vector2 p in drawTest)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(p, new Vector3(1.25f, 1.25f, 0));
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public LinePoints AddNewLinePoint(Vector2 point)
    {
        LinePoints linePoints;

        PathGraphVertex vert = new PathGraphVertex(point);

        
            
        if (_lines.Count > 0)
        {
            vert.addNeighbour(lastVertex);
            // Instantiate new line between the last point in the list and the current point.
            Line line = Instantiate<Line>(linePrefab, lineContainer);

            linePoints = new LinePoints(LastLinePoint.end, point);

            line.Initialize(linePoints);
            line.SetVertices(lastVertex, vert);
            line.colliderEnableDelay = colliderEnableDelay;

            
            
            Debug.Log($"Line added: '{LastLinePoint} TO {point}'", line);
        }
        else
        {
            linePoints = new LinePoints(point);
        }
            
        
        _lines.Add(linePoints);

        _vertices.Add(vert);
        
        return linePoints;
    }
    
    public void AddNewLineIntersection(Vector2 collisionIntersection, Line line)
    {
        bool isVertical = line.start.x == line.end.x; 
        
        Vector2 intersection = isVertical? new Vector2(line.start.x,collisionIntersection.y) : new Vector2(collisionIntersection.x,line.start.y); 
        
        AddNewLinePoint(intersection);

        lastVertex.intersectWith(line);
        
        _lines[_lines.Count - 1].intersectedLine = line;

        List<Vector2> result = new List<Vector2>();
        result.Add(intersection);

        for (int i = _lines.Count - 2; i > 0; i--)
        {
            result.Add(_lines[i].end);

            //if (_lines[i].isIntersection)
            //    _lines[i].intersectedLine.

            if (_lines[i].end == line.end)
            {
                remTest= _lines[i].end;
                //_lines[i].end = intersection;
                intersectionTest =intersection;
                
                break;
            }
        }

        drawTest = result;
        OnEnclose?.Invoke(new IntersectionArgs(result.ToArray()));
    }
}

public struct IntersectionArgs
{
    public IntersectionArgs(Vector2[] points)
    {
        this.points = points;
    }

    public Vector2[] points { get; private set; }
}
