using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    [SerializeField] private string _imageName = "cameracapture";
    public static Texture2D activeScreenshot = null;

    private bool canTakeScreenshot = true;

    /**
     * Uses the attached camera to capture a screenshot.
     * Currently the screenshot is available as a PNG byte array or a Texture2D, neither of which is passed on.
     * Instead the screenshot is saved into the application data path for testing purposes.
     * The Texture is also saved into a static field to be used in the score scene.
     * 
     * IMPORTANT: currently the sceenshot isn't attached to any game functionality and can be taken once per game by pressing 'K'.
     * Please remove that functionality from update() when assigning a purpose for this function.
     */
    public void captureCameraView()
    {
        Camera camera = GetComponent<Camera>();
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        camera.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        camera.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;

        activeScreenshot = renderedTexture;
        byte[] byteArray = renderedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + $"/{_imageName}.png", byteArray);
        Debug.Log("Screenshot saved at: " + Application.dataPath + $"/{_imageName}.png");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && canTakeScreenshot)
        {
            canTakeScreenshot = false;
            captureCameraView();
        }
    }
}
