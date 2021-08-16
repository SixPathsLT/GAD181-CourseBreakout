using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityChanger : MonoBehaviour
{
    [SerializeField] Slider sensitivitySlider;
    public Settings settings;

    
    public void ChangeSenitivity()
    {
        settings.sensetivity = sensitivitySlider.value;       
    }
}
