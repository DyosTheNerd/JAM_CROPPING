/*
* Copyright (c) Marvin Woelke
* PauseManager 2021
*/

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public static class PauseHandler
{
    public static bool isPaused
    {
        get
        {
            return Time.timeScale == 0;
        }
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public static void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public static void Resume()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Toggle pauses.
    /// </summary>
    /// <returns>False = Paused | Ture = Unpaused</returns>
    public static bool TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        return !isPaused;
    }
}
