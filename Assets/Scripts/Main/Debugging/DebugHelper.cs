using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelper : MonoBehaviour
{
    public void DebugLog(string message)
    {
        Debug.Log(message);
    }

    public void DebugLogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public void DebugLogError(string message)
    {
        Debug.LogError(message);
    }
}
