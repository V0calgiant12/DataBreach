using System;
using Unity.VisualScripting;

//using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    //[SerializeField] private TextMeshProUGUI ValueText;
    [SerializeField] private VolumeType type;
    [SerializeField] private Slider slider;
    public float defaultValue = 0.5f;
    public enum VolumeType
    {
        Master,
        Music,
        Effects,
        Dialogue
    }
    public void SaveSettings()
    {
        switch (type)
        {
            case VolumeType.Master:
                SettingsData.Instance._MasterVolume = slider.value;
                break;
            case VolumeType.Music:
                SettingsData.Instance._MusicVolume = slider.value;
                break;
            case VolumeType.Effects:
                SettingsData.Instance._EffectsVolume = slider.value;
                break;
            case VolumeType.Dialogue:
                SettingsData.Instance._DialogueVolume = slider.value;
                break;
        }
    }
    public void LoadSettings()
    {
        switch (type)
        {
            case VolumeType.Master:
                slider.value = SettingsData.Instance._MasterVolume;
                break;
            case VolumeType.Music:
                slider.value = SettingsData.Instance._MusicVolume;
                break;
            case VolumeType.Effects:
                slider.value = SettingsData.Instance._EffectsVolume;
                break;
            case VolumeType.Dialogue:
                slider.value = SettingsData.Instance._DialogueVolume;
                break;
        }
        OnChangeSlider(slider.value);
    }
    public void OnChangeSlider(float Value)
    {
        //ValueText.text = "Master Volume: " + $"{Value.ToString("N4")}";
        switch (type)
        {
            case VolumeType.Master:
                Mixer.SetFloat("MasterVolume",MathF.Log10(Value)*20);
                break;
            case VolumeType.Music:
                Mixer.SetFloat("MusicVolume",MathF.Log10(Value)*20);
                break;
            case VolumeType.Effects:
                Mixer.SetFloat("EffectsVolume",MathF.Log10(Value)*20);
                break;
            case VolumeType.Dialogue:
                Mixer.SetFloat("DialogueVolume",MathF.Log10(Value)*20);
                break;
        }
        
    }
    public void ResetToDefault()
    {
        slider.value = defaultValue;
        OnChangeSlider(slider.value);
    }
}
