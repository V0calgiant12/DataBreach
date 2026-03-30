using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    public void GrassSound(AudioClip audioClip,float volume)
    {
        audioSource.pitch = Random.Range(0.8f,1.5f);
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void JumpSound(AudioClip audioClip,float volume)
    {
        audioSource.pitch = Random.Range(0.7f,1.4f);
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void StoneSound(AudioClip audioClip,float volume)
    {
        audioSource.pitch = Random.Range(0.6f,1.3f);
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