using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AppVersionText : MonoBehaviour
{
    [SerializeField] private string prefix = "v ";
    [SerializeField] private string suffix = string.Empty;

    private void Awake()
    {
        GetComponent<Text>().text = string.Format("{0}{1}{2}", prefix, Application.version, suffix);
    }
}
