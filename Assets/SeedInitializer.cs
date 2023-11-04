using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SeedInitializer : MonoBehaviour
{
    private Slider _slider;
    
    void Start()
    {
        _slider = GetComponent<Slider>();

        SeedSingleton.getInstance().seed = (uint)_slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUpdateSlider()
    {
        SeedSingleton.getInstance().seed = (uint)_slider.value;
    }
    
}
