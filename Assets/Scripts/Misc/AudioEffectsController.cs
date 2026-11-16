using System;
using UnityEditor.Audio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEffectsController : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    public static AudioEffectsController Instance;
    public static bool LowpassEffect = false;
    private void Awake()
    {
        Instance = this;
    }
    public void UpdateEffects()
    {
        Mixer.SetFloat("Lowpass",LowpassEffect?1000f:22000f);
        Mixer.GetFloat("Lowpass",out float lowpassVal);
        //Debug.Log("Set Lowpass to: " + lowpassVal);
    } 
}