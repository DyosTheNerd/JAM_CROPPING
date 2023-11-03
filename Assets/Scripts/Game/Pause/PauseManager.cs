/* Copyright (c) Marvin Woelke
* PauseManager 2023
*/

using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [Space]
    public bool canBePaused;

    [Header("Events")]
    public UnityEvent onPause;
    public UnityEvent onResume;

    void Update()
    {
        if (!this.isActiveAndEnabled || !canBePaused)
            return;

        if (Input.GetKeyDown(pauseKey))
        {
            if (PauseHandler.TogglePause())
                onResume?.Invoke();
            else
                onPause?.Invoke();
        }
    }
}
