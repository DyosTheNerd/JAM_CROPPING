using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class Line : MonoBehaviour
{
    //private EdgeCollider2D _edgeCollider;

    [FormerlySerializedAs("edgeCollider")]
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField]
    private float traceWidth;
    
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
        boxCollider.enabled = true;
    }

    public void Initialize(Vector2 pointStart, Vector2 pointEnd)
    {
        Initialize(new LinePoints(pointStart, pointEnd));
    }

    public void Initialize(LinePoints linePoints)
    {
        points = linePoints;



        InitializeBox(start, end);
        
    }

    private void InitializeBox(Vector2 start, Vector2 end)
    {
        float edgeDist = traceWidth * 0.5f;
        float top = Math.Max(start.y + edgeDist, end.y + edgeDist);
        float bottom =  Math.Min(start.y - edgeDist, end.y - edgeDist);
        float left = Math.Min(start.x - edgeDist, end.x - edgeDist);
        float right = Math.Max(start.x + edgeDist, end.x + edgeDist);

        transform.localScale = new Vector3(right - left, top - bottom, 0);

        transform.position = new Vector3((left + right) / 2, (top + bottom) / 2, 0);
        boxCollider.enabled = false;
    }

    public void SetVertices(PathGraphVertex v1,PathGraphVertex v2)
    {
        myVertex1 = v1;
        myVertex2 = v2;
    }

    public PathGraphVertex myVertex1;
    public PathGraphVertex myVertex2;
    
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