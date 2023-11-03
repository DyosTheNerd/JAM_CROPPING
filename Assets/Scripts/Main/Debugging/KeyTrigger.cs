using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

//[Flags]
public enum KeyInputType
{
    Down,
    Up,
    Hold
}

//public enum UpdateType
//{
//    Update,
//    FixedUpdate,
//    LateUpdate
//}

public class KeyTrigger : MonoBehaviour
{
    [SerializeField] private List<KeyEventHandler> keyEvents;

    void Update()
    {
        foreach (KeyEventHandler keyEvent in keyEvents)
        {
            switch (keyEvent.InputType)
            {
                case KeyInputType.Down:
                    if (Input.GetKeyDown(keyEvent.KeyCode))
                        keyEvent.OnKeyEvent?.Invoke();
                    break;
                case KeyInputType.Up:
                    if (Input.GetKeyUp(keyEvent.KeyCode))
                        keyEvent.OnKeyEvent?.Invoke();
                    break;
                case KeyInputType.Hold:
                    if (Input.GetKey(keyEvent.KeyCode))
                        keyEvent.OnKeyEvent?.Invoke();
                    break;
            }
        }
    }
}

[Serializable]
public class KeyEventHandler
{
    [SerializeField] private KeyCode keyCode = KeyCode.Space;
    [SerializeField] private KeyInputType inputType = KeyInputType.Down;
    [Space]
    public UnityEvent OnKeyEvent;
    public KeyCode KeyCode => keyCode;
    public KeyInputType InputType => inputType;
}
