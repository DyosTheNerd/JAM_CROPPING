// Copyright (c) Marvin Woelke 2023 //
// Line v1.0.0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Line : MonoBehaviour
{
    //private EdgeCollider2D _edgeCollider;

    [Header("References")]
    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Settings")]
    public float colliderEnableDelay = 0.1f;

    public LinePoints points;

    public Vector2 start => points.start;
    public Vector2 end => points.end;

    // REMOVE
    //public LinePoints pointStart { get; private set; }
    //public LinePoints pointEnd { get; private set; }

    IEnumerator Start()
    {
        // TODO Distance!
        yield return new WaitForSeconds(colliderEnableDelay);
        edgeCollider.enabled = true;
    }

    public void Initialize(Vector2 pointStart, Vector2 pointEnd)
    {
        Initialize(new LinePoints(pointStart, pointEnd));
    }

    public void Initialize(LinePoints linePoints)
    {
        points = linePoints;

        edgeCollider.SetPoints(new List<Vector2> { start, end });

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MAIN.TAGS.PLAYER))
        {
            //Line line = collision.GetComponent<Line>();

            Debug.Log($"Player: {points.start} - {points.end}", this);
            //LineManager.Instance.AddNewLinePoint(collision.transform.position);
            LineManager.Instance.AddNewLineIntersection(collision.transform.position, this);
        }
    }
}

public class LinePoint
{
    public LinePoint(Vector2 point)
    {
        this.point = point;
    }

    public Vector2 point { get; private set; }

    public LinePoint up { get; set; }
    public LinePoint down { get; set; }
    public LinePoint left { get; set; }
    public LinePoint right { get; set; }
}

public class LinePoints
{
    public LinePoints(Vector2 start)
    {
        this.start = start;
        this.end = start;
        this.intersectedLine = null;
    }

    public LinePoints(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
        this.intersectedLine = null;
    }

    public LinePoints(Vector2 previousPoint, Vector2 currentPoint, Line line)
    {
        this.start = previousPoint;
        this.end = currentPoint;
        this.intersectedLine = line;
    }

    public Vector2 start { get; set; }
    public Vector2 end { get; set; }
    public Line intersectedLine { get; set; }

    public bool isIntersection => intersectedLine != null;
}