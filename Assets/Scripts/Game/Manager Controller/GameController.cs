using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private float _gameTime = 0f;

    public float gameTime => _gameTime;

    public UnityEvent onGameStart;
    public UnityEvent onGameOver;

    private void Awake()
    {
        UIManager.CursorActive(false);
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
    }

    public void StartGame()
    {
        Debug.Log(" - GAME START -");
        onGameStart?.Invoke();
    }

    public void GameOver()
    {
        Debug.Log(" - GAME OVER -");
        onGameStart?.Invoke();
    }
}
