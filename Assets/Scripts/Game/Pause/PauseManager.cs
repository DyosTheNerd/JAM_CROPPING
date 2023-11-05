
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private KeyCode quitKey = KeyCode.Q;
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

        if (Input.GetKeyDown(quitKey))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
