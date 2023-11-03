/*
* Copyright (c) Marvin Woelke
* MAIN 2021
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class MAIN
{
    private static Camera _camera;
    private static readonly Dictionary<float, WaitForSeconds> _waitDictionary = new Dictionary<float, WaitForSeconds>();
    private static PointerEventData _eventDataCurrentPosition;

    #region CONST STRING REFERENCES
    public struct TAGS
    {
        public const string PLAYER = "Player";
    }

    public struct LAYERS
    {

    }

    public struct INPUT
    {
        public struct AXIS
        {
            public const string HORIZONTAL = "Horizontal";
            public const string VERTICAL = "Vertical";
        }

        public struct BUTTON
        {
            public const string CANCEL = "Cancel";
        }
    }
    #endregion

    #region MAIN METHODS
    /// <summary>
    /// Returns the current main camera.
    /// </summary>
    public static Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;

            return _camera;
        }
    }

    public static WaitForSeconds GetWait(float time)
    {
        if (_waitDictionary.TryGetValue(time, out var wait))
            return wait;

        _waitDictionary[time] = new WaitForSeconds(time);
        return _waitDictionary[time];
    }

    /// <summary>
    /// Checks if mouse/finger is over an UI element.
    /// </summary>
    /// <returns>true = over UI | false = not over UI</returns>
    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> _results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    /// <summary>
    /// Gets the world position of an element on a canvas.
    /// </summary>
    /// <param name="element">Element on a canvas.</param>
    /// <returns>World position of the element.</returns>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }

    /// <summary>
    /// Returns a random bool value.
    /// </summary>
    /// <returns>True/False</returns>
    public static bool GetRandomBool()
    {
        return Random.value > 0.5f;
    }
    #endregion
}