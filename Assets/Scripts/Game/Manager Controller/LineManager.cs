// Copyright (c) Marvin Woelke 2023 //
// LineManager v1.0.0

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance { get; private set; }

    private List<LinePoints> _lines = new List<LinePoints>();
    //private List<Line> _lines = new List<Line>();

    [Header("References")]
    [SerializeField] private Transform lineContainer;
    [SerializeField] private Line linePrefab;
    [SerializeField] private Transform CornerPrefab;

    [Header("Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private float colliderEnableDelay = 0.1f;

    public delegate void EncloseEvent(IntersectionArgs data);

    public event EncloseEvent OnEnclose;
    
    public LinePoints LastLinePoint => _lines[_lines.Count - 1];
    //public Line LastLine => _lines[_lines.Count - 1];

    List<Vector2> intersectionTest = new List<Vector2>();
    List<Vector2> remTest = new List<Vector2>();
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


            foreach (Vector2 p in intersectionTest)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(p, 1.2f);
            }

            foreach (Vector2 p in remTest)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(p, 1.2f);
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

        // checks if the line point array has more than 0 points (to create a line!)
        if (_lines.Count > 0)
        {
            // Instantiate new line between the last point in the list and the current point.
            Line line = Instantiate<Line>(linePrefab, lineContainer);

            linePoints = new LinePoints(LastLinePoint.end, point);

            line.Initialize(linePoints);
            line.colliderEnableDelay = colliderEnableDelay;

            InitializeCorner(point);
            
            Debug.Log($"Line added: '{LastLinePoint} TO {point}'", line);
        }
        else
            linePoints = new LinePoints(point);

        _lines.Add(linePoints);

        return linePoints;
    }

    /** To keep the corner sharp */
    private void InitializeCorner(Vector2 point)
    {
        Transform newCorner = Instantiate(CornerPrefab);
        newCorner.position = new Vector3(point.x, point.y, 0);
        newCorner.SetParent(lineContainer);
    }
    
    
    public void AddNewLineIntersection(Vector2 intersection, Line line)
    {
        AddNewLinePoint(intersection);

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
                remTest.Add(_lines[i].end);
                //_lines[i].end = intersection;
                intersectionTest.Add(intersection);
                Debug.Break();
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
