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

    [Header("References")] [SerializeField]
    private Transform lineContainer;

    [SerializeField] private Line linePrefab;
    [SerializeField] private Transform CornerPrefab;

    [Header("Settings")] [SerializeField] private bool debug = true;
    [SerializeField] private float colliderEnableDelay = 0.1f;

    public delegate void EncloseEvent(IntersectionArgs data);

    public event EncloseEvent OnEnclose;

    public PathGraphVertex lastVertex => _vertices[^1];
  
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

            if (remTest != null)
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

            linePoints = new LinePoints(lastVertex.position, point);

            line.Initialize(linePoints);
            line.SetVertices(lastVertex, vert);
            line.colliderEnableDelay = colliderEnableDelay;


           
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
        PathGraphVertex start = lastVertex;

        bool isVertical = line.start.x == line.end.x;

        bool movesVertical = start.position.x == collisionIntersection.x;

        if (isVertical == movesVertical)
        {
            // ignore collisions in the same direction
            return;
        }
        
        Vector2 intersection = isVertical
            ? new Vector2(line.start.x, collisionIntersection.y)
            : new Vector2(collisionIntersection.x, line.start.y);

        AddNewLinePoint(intersection);

        lastVertex.intersectWith(line);

        SplitLine(line, lastVertex);

        List<PathGraphVertex> result = new List<PathGraphVertex>();


        PathGraphVertex toFind = lastVertex;

        PathGraphVertex current = start;
        PathGraphVertex comingFrom = lastVertex;

        result.Add(toFind);

        int iterations = 0;
        
        while (current != toFind && current != null && iterations < _vertices.Count)
        {
            
            result.Add(current);

            PathGraphVertex next = current.traceBack(comingFrom);
            comingFrom = current;
            current = next;
            iterations += 1;
        }

        List<Vector2> newDrawTest = new List<Vector2>();

        for (int i = 0; i < result.Count; i++)
        {
            newDrawTest.Add(result[i].position);
        }

        remTest = result[0].position;
        intersectionTest = result[^1].position;


        drawTest = newDrawTest;
        OnEnclose?.Invoke(new IntersectionArgs(result.ToArray()));
    }

    void SplitLine(Line line, PathGraphVertex vert)
    {
        _lines.Remove(line.points);
        

        Line line1 = Instantiate<Line>(linePrefab, lineContainer);

        LinePoints linePoints1 = new LinePoints(line.myVertex1.position, vert.position);

        line1.Initialize(linePoints1);
        line1.SetVertices(line.myVertex1, vert);
        line1.colliderEnableDelay = colliderEnableDelay;
        

        _lines.Add(linePoints1);
        
        Line line2 = Instantiate<Line>(linePrefab, lineContainer);

        LinePoints linePoints2 = new LinePoints(line.myVertex2.position, vert.position);

        line2.Initialize(linePoints2);
        line2.SetVertices(line.myVertex2, vert);
        line2.colliderEnableDelay = colliderEnableDelay;
        

        _lines.Add(linePoints2);
        

        Destroy(line);
    }
}

public struct IntersectionArgs
{
    public IntersectionArgs(PathGraphVertex[] points)
    {
        this.points = points;
    }

    public PathGraphVertex[] points { get; private set; }
}