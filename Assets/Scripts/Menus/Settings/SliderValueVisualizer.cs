using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueVisualizer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField text;
    [SerializeField] private string specialMaxText = "";
    [SerializeField] private string specialMinText = "";
    void OnGUI()
    {
        if(!text.isFocused)
        {
            text.text = slider.value.ToString();
            if(slider.value == slider.maxValue && specialMaxText != "")
            {
                text.text = specialMaxText;
            }
            if(slider.value == slider.minValue && specialMinText != "")
            {
                text.text = specialMinText;
            }
        }
    }
}