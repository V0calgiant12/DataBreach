using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingSliderData : MonoBehaviour
{
    public Slider slider;
    public float _DefaultValue = 0.5f;
    public int _SliderID;
    public float _StepSize = 1f;
    public void RefreshVisuals() // Refreshes the slider state to be up to date with the saves
    {
        switch (_SliderID)
        {
            case (0):
                slider.value = SettingsData.Instance._CameraZoom;
                break;
            case (1):
                slider.value = SettingsData.Instance._PlayerHue;
                break;
            case (2):
                slider.value = SettingsData.Instance._PlayerSaturation;
                break;
            case (3):
                slider.value = SettingsData.Instance._PlayerValue;
                break;
            case (4):
                slider.value = SettingsData.Instance._MaxFrameRate;
                break;
        }
    }
    public void InputValue(string value)
    {
        float newValue = slider.value;
        if(!float.TryParse(value,out newValue))
        {
            return;
        }
        if(newValue < slider.minValue)
        {
            newValue = slider.minValue;
        }
        if(newValue > slider.maxValue)
        {
            newValue = slider.maxValue;
        }
        slider.value = newValue;
    }
}
