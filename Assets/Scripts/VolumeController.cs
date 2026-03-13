using System;
using Unity.VisualScripting;

//using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    //[SerializeField] private TextMeshProUGUI ValueText;
    [SerializeField] private VolumeType type;
    public enum VolumeType
    {
        Master,
        Music,
        Effects,
        Dialogue
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
}
