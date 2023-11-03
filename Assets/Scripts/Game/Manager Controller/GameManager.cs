using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    //[SerializeField] internal PlayerController playerController;

    [Header("Controller References")]
    [SerializeField] internal GameController gameController;

    [Header("Manager References")]
    [SerializeField] internal UIManager uiManager;
    [SerializeField] internal PauseManager pauseManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public static void LoadScene(int sceneBuildIndex)
    {
        print("Load Scene with Build Index: " + sceneBuildIndex);
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
