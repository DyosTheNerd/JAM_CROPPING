using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool triggerOnStart = true;
    [SerializeField] private bool triggerOnEnabled = true;
    [SerializeField] private bool triggerOnDisabled = false;
    [Space]
    [SerializeField] private bool loop = false;
    [Space]
    [SerializeField][Min(0)] private float time = 1f;
    [Space]

    public UnityEvent onTrigger;
    private void Start()
    {
        if (triggerOnStart)
            if (time == 0f)
                onTrigger?.Invoke();
            else
                StartCoroutine(TriggerCoroutine());
    }

    private void OnEnable()
    {
        if (triggerOnEnabled)
            if (time == 0f)
                onTrigger?.Invoke();
            else
                StartCoroutine(TriggerCoroutine());
    }

    private void OnDisable()
    {
        if (triggerOnDisabled)
            if (time == 0f)
                onTrigger?.Invoke();
            else
                StartCoroutine(TriggerCoroutine());
    }


    private IEnumerator TriggerCoroutine()
    {
        do
        {
            if (time > 0f)
                yield return new WaitForSeconds(time);

            onTrigger?.Invoke();
        } while (loop);
    }
}
