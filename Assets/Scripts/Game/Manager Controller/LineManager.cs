// Copyright (c) Marvin Woelke 2023 //
// LineManager v1.0.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public static LineManager Instance { get; private set; }

    //private List<Vector2> _linePoints = new List<Vector2>();
    private List<LinePoint> _linePoints = new List<LinePoint>();

    [Header("References")]
    [SerializeField] private Transform lineContainer;
    [SerializeField] private Line linePrefab;

    [Header("Settings")]
    [SerializeField] private bool debug = true;
    [SerializeField] private float colliderEnableDelay = 0.1f;

    public LinePoint LastLinePoint => _linePoints[_linePoints.Count - 1];

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        foreach (LinePoint lp in _linePoints)
        {
            Gizmos.color = lp.isIntersection ? Color.red : Color.green;
            Gizmos.DrawCube(lp.point, new Vector3(1, 1, 0));

            Gizmos.color = new Color(1, 1, 1, 0.25f);
            Gizmos.DrawLine(lp.pointPrevious, lp.point);

            if (lp.isIntersection)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lp.point, lp.pointClockwise.Value);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(lp.point, lp.pointAntiClockwise.Value);
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

    public LinePoint AddNewLinePoint(Vector2 point)
    {
        LinePoint newPoint;

        // checks if the line point array has more than 0 points (to create a line!)
        if (_linePoints.Count > 0)
        {
            // Instantiate new line between the last point in the list and the current point.
            Line line = Instantiate<Line>(linePrefab, lineContainer);
            line.Initialize(LastLinePoint.point, point);
            line.colliderEnableDelay = colliderEnableDelay;

            newPoint = new LinePoint(LastLinePoint.point, point);

            Debug.Log($"Line added: '{LastLinePoint} TO {point}'", line);
        }
        else
            newPoint = new LinePoint(point);

        _linePoints.Add(newPoint);

        return newPoint;
    }

    public void AddNewLineIntersection(Vector2 intersection, Line line)
    {
        // checks if the line point array has more than 0 points (to create a line!)

        // Instantiate new line between the start and the intersection.

        AddNewLinePoint(intersection);

        _linePoints[_linePoints.Count - 1].pointClockwise = line.pointStart;
        _linePoints[_linePoints.Count - 1].pointAntiClockwise = line.pointEnd;
    }
}
