// Copyright (c) Marvin Woelke 2023 //
// InputManager v1.0.0

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class InputManager : MonoBehaviour
{
    private PlayerController _player;

    [Header("Settings")]
    [SerializeField] private float inputDelayDisntace = 1f;
    [SerializeField] private bool clockwise = false;

    public Vector2 direction { get; private set; }
    public Vector2 inputAxis { get; private set; }

    public event Action OnDirectionChanged;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        direction = _player.startDirection;
    }

    private void Update()
    {
        if (Vector2.Distance(this.transform.position, LineManager.Instance.LastLinePoint.point) >= inputDelayDisntace)
        {
            Vector2 newInputAxis = new Vector2(Input.GetAxisRaw(MAIN.INPUT.AXIS.HORIZONTAL), Input.GetAxisRaw(MAIN.INPUT.AXIS.VERTICAL));

            if (Input.GetButtonDown(MAIN.INPUT.BUTTON.ENTER)) //Input.GetButton(MAIN.INPUT.BUTTON.ENTER);
            {
                SwitchDirection(clockwise);
            }

            if (newInputAxis != inputAxis)
                if (HandleNewInput(newInputAxis))
                    OnDirectionChanged?.Invoke();
        }
    }

    private void SwitchDirection(bool clockwise)
    {
        if (direction == Vector2.up) direction = clockwise ? Vector2.right : Vector2.left;
        else if (direction == Vector2.down) direction = clockwise ? Vector2.left : Vector2.right;
        else if (direction == Vector2.left) direction = clockwise ? Vector2.up : Vector2.down;
        else if (direction == Vector2.right) direction = clockwise ? Vector2.down : Vector2.up;

        OnDirectionChanged?.Invoke();
    }

    private bool HandleNewInput(Vector2 input)
    {
        if (direction == Vector2.up || direction == Vector2.down)
        {
            if (input == Vector2.left)
            {
                direction = Vector2.left;
                return true;
            }
            else if (input == Vector2.right)
            {
                direction = Vector2.right;
                return true;
            }

        }
        else if (direction == Vector2.left || direction == Vector2.right)
        {
            if (input == Vector2.up)
            {
                direction = Vector2.up;
                return true;
            }
            else if (input == Vector2.down)
            {
                direction = Vector2.down;
                return true;
            }
        }

        return false;
    }
}
