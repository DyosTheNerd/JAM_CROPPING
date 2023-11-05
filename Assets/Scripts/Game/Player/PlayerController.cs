// Copyright (c) Marvin Woelke 2023 //
// PlayerController v1.0.0

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;

    [Header("References")]
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Settings")]
    public bool enableMovement = true;
    [SerializeField] internal Vector2 startDirection = Vector2.up;
    [SerializeField] private float speed = 100f;

    private Camera mainCamera;
    
    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.OnDirectionChanged += InputManager_DirectionChanged;

        LineManager.Instance.AddNewLinePoint(this.transform.position);
        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        _inputManager.OnDirectionChanged -= InputManager_DirectionChanged;
    }

    private void Update()
    {
        if (enableMovement)
            this.transform.Translate(_inputManager.direction * speed * Time.deltaTime);

        if (isOutsideOfScreen())
        {
            if (wasInside)
            {
                LevelManager.instance.triggerLevelEnd();
            }
        }
        else
        {
            wasInside = true;
        }

    }

    private bool wasInside = false;
    
    private bool isOutsideOfScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the object's position is outside the camera's view frustum
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1 || screenPoint.z < 0)
        {
            return true;
        }

        return false;
    }
    
    private void InputManager_DirectionChanged()
    {
        LineManager.Instance.AddNewLinePoint(this.transform.position);
    }
}
