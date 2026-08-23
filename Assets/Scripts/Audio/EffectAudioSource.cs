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
        audioSource.spatialBlend = 0;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void PlaySound(AudioClip audioClip,float volume, float pitch, float spatialBlend, float distanceModifier)
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.spatialBlend = spatialBlend;
        audioSource.maxDistance = audioSource.maxDistance * distanceModifier;
        audioSource.minDistance = audioSource.minDistance * distanceModifier;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void SlimeJumpSound(AudioClip audioClip,float volume)
    {
        audioSource.pitch = Random.Range(0.7f,1.3f);
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 1;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void MudSound(AudioClip audioClip,float volume)
    {
        audioSource.pitch = Random.Range(0.8f,1.2f);
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 1;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    IEnumerator Delete()
    {
        if(audioSource.clip != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        Destroy(gameObject);
    }
}