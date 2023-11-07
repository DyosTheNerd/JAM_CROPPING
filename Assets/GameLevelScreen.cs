using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GameLevelScreen : MonoBehaviour
{
    public event Action ClickableClicked;
    
    void OnMouseDown()
    {
        ClickableClicked?.Invoke();
    }
}
