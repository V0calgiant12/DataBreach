using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class EffectAudioSource : MonoBehaviour
{
    [Header("Effect Audio Source References:")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    public void HeartSound(AudioClip audioClip,float volume)
    {
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}