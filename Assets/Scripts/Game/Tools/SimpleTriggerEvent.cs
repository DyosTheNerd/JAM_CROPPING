/*
* Copyright (c) Marvin Woelke
* SimpleTriggerEvent 2021
*/

//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class SimpleTriggerEvent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<string> tags;
    [SerializeField] private bool disableAfterEnter;
    [SerializeField] private bool disableAfterExit;

    [Header("Unity Trigger Events")]
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        if (tags != null && tags.Count > 0)
        {
            if (tags.Contains(other.tag))
            {
                onTriggerEnter?.Invoke();

                if (disableAfterEnter)
                    gameObject.SetActive(false);
            }
        }
        else
        {
            onTriggerEnter?.Invoke();

            if (disableAfterEnter)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (tags != null && tags.Count > 0)
            if (tags.Contains(other.tag))
                onTriggerStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (tags != null && tags.Count > 0)
        {
            if (tags.Contains(other.tag))
            {
                onTriggerExit?.Invoke();

                if (disableAfterExit)
                    gameObject.SetActive(false);
            }
        }
        else
        {
            onTriggerExit?.Invoke();

            if (disableAfterExit)
                gameObject.SetActive(false);
        }
    }
}
