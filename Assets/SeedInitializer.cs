
using TMPro;
using UnityEngine;



public class SeedInitializer : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    
    void Start()
    {
        
        
        uint randomSeed = SeedSingleton.getInstance().getInitialRandomSeed();

        _inputField.text = "" + randomSeed;

        SeedSingleton.getInstance().seed = randomSeed;
    }

    
    public void OnUpdateSeed()
    {
        SeedSingleton.getInstance().seed = uint.Parse(_inputField.text);
    }
    
}
