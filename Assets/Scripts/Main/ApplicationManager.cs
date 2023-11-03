using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
	private static bool applicationFrozen = false;
	
    public UnityEvent onStart;
    public UnityEvent onApplicationQuit;
    public UnityEvent onApplicationGetsFocus;
    public UnityEvent onApplicationLoseFocus;

    private void Start()
    {
        onStart?.Invoke();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            onApplicationGetsFocus?.Invoke();
        else
            onApplicationLoseFocus?.Invoke();
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadScene(int buildIndex, LoadSceneMode loadMode)
    {
        SceneManager.LoadScene(buildIndex, loadMode);
    }

    public void Quit()
    {
        onApplicationQuit?.Invoke();
        Application.Quit();
    }

    public void Quit(int exitCode)
    {
        onApplicationQuit?.Invoke();
        Application.Quit(exitCode);
    }

    public void FreezeApplication(float time)
    {
        if (!applicationFrozen)
            StartCoroutine(FreezeApplicationCoroutine(time));
    }

    private static IEnumerator FreezeApplicationCoroutine(float time)
    {
        applicationFrozen = true;
        float originTime = Time.timeScale;

        Time.timeScale = 0f;
        yield return new WaitForSeconds(time);
        Time.timeScale = originTime;

        applicationFrozen = false;
    }
}
