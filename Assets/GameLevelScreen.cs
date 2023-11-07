using System;
using UnityEngine;

public class GameLevelScreen : MonoBehaviour
{
    public event Action ClickableClicked;
    
    void OnMouseDown()
    {
        ClickableClicked?.Invoke();
    }
}
