using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioScource;

    private static bool isPlaying = false;

    private void Awake()
    {
        if (!isPlaying)
        {
            audioScource.Play();
            isPlaying = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (!isPlaying)
        {
            audioScource.Play();
            isPlaying = true;
        }
    }

    private void OnApplicationQuit()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
