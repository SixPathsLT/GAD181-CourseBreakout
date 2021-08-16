using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public void VolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
