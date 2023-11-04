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

    public Vector2 pointStart { get; private set; }
    public Vector2 pointEnd { get; private set; }

    IEnumerator Start()
    {
        // TODO Distance!
        yield return new WaitForSeconds(colliderEnableDelay);
        edgeCollider.enabled = true;
    }

    public void Initialize(Vector2 pointA, Vector2 pointB)
    {
        edgeCollider.SetPoints(new List<Vector2> { pointA, pointB });

        lineRenderer.SetPosition(0, pointA);
        lineRenderer.SetPosition(1, pointB);

        pointStart = pointA;
        pointEnd = pointB;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(MAIN.TAGS.PLAYER))
        {
            //Line line = collision.GetComponent<Line>();

            Debug.Log($"Player: {pointStart} - {pointEnd}", this);
            //LineManager.Instance.AddNewLinePoint(collision.transform.position);
            LineManager.Instance.AddNewLineIntersection(collision.transform.position, this);
        }
    }
}

public class LineData
{

}

public class LinePoint
{
    public LinePoint(Vector2 currentPoint)
    {
        this.pointPrevious = currentPoint;
        this.point = currentPoint;
        this.pointClockwise = null;
        this.pointAntiClockwise = null;
    }

    public LinePoint(Vector2 previousPoint, Vector2 currentPoint)
    {
        this.pointPrevious = previousPoint;
        this.point = currentPoint;
        this.pointClockwise = null;
        this.pointAntiClockwise = null;
    }

    public LinePoint(Vector2 previousPoint, Vector2 currentPoint, Vector2 clockwisePoint, Vector2 antiClockwisePoint)
    {
        this.pointPrevious = previousPoint;
        this.point = currentPoint;
        this.pointClockwise = clockwisePoint;
        this.pointAntiClockwise = antiClockwisePoint;
    }

    public Vector2 point { get; set; }
    public Vector2 pointPrevious { get; set; }
    public Vector2? pointClockwise { get; set; }
    public Vector2? pointAntiClockwise { get; set; }

    public bool isIntersection => pointClockwise.HasValue & pointAntiClockwise.HasValue;
}