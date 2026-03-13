using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private TextMeshProUGUI ValueText;
    public void OnChangeSlider(float Value)
    {
        //ValueText.text = "Master Volume: " + $"{Value.ToString("N4")}";
        Mixer.SetFloat("Volume",MathF.Log10(Value)*20);
    }
}
