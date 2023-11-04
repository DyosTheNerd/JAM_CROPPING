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

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _inputManager.OnDirectionChanged += InputManager_DirectionChanged;

        LineManager.Instance.AddNewLinePoint(this.transform.position);
    }

    private void OnDisable()
    {
        _inputManager.OnDirectionChanged -= InputManager_DirectionChanged;
    }

    private void Update()
    {
        if (enableMovement)
            this.transform.Translate(_inputManager.direction * speed * Time.deltaTime);
    }

    private void InputManager_DirectionChanged()
    {
        LineManager.Instance.AddNewLinePoint(this.transform.position);
    }
}
