// Copyright (c) Marvin Woelke 2023 //
// PlayerController v1.0.0

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down
}

[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    private float _lastInput;
    private Vector2 _direction;
    private Vector2 _inputAxis = Vector2.zero;

    private List<Vector2> _crossPoints = new List<Vector2>();

    [Header("References")]
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Settings")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float inputDelay = 1f;
    [SerializeField] private float inputDelayDisntace = 1f;

    [SerializeField] private Vector2 startDirection = Vector2.up;

    public Vector2 direction => _direction;
    public Vector2 LastPosition => _crossPoints[_crossPoints.Count - 1];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (Vector2 position in _crossPoints)
            Gizmos.DrawCube(position, new Vector3(1, 1, 0));
    }

    private void Start()
    {
        _direction = startDirection;
        _crossPoints.Add(this.transform.position);
        _lastInput = inputDelay;
        //lineRenderer.SetPosition(0, transform.position);
    }

    private void Update()
    {
        //if (_lastInput > 0)
        //    _lastInput -= Time.deltaTime;
        if (Vector2.Distance(this.transform.position, LastPosition) >= inputDelayDisntace)
        {
            Vector2 newInputAxis = new Vector2(Input.GetAxisRaw(MAIN.INPUT.AXIS.HORIZONTAL), Input.GetAxisRaw(MAIN.INPUT.AXIS.VERTICAL));

            if (newInputAxis != _inputAxis)
            {
                if (HandleNewInput(newInputAxis))
                {
                    _crossPoints.Add(this.transform.position);
                    _lastInput = inputDelay;
                }
            }
        }
        
        this.transform.Translate(_direction * speed * Time.deltaTime);
    }

    private bool HandleNewInput(Vector2 input)
    {
        if (_direction == Vector2.up || _direction == Vector2.down)
        {
            if (input == Vector2.left)
            {
                _direction = Vector2.left;
                return true;
            }
            else if (input == Vector2.right)
            {
                _direction = Vector2.right;
                return true;
            }

        }
        else if (_direction == Vector2.left || _direction == Vector2.right)
        {
            if (input == Vector2.up)
            {
                _direction = Vector2.up;
                return true;
            }
            else if (input == Vector2.down)
            {
                _direction = Vector2.down;
                return true;
            }
        }

        return false;
    }
}
