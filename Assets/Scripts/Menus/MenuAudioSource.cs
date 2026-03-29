using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MenuAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    public void TextSound(TextWrite data)
    {
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Dialogue")[0];
        audioSource.clip = data._TextSound;
        audioSource.Play();
        StartCoroutine(Delete());
    }
    public void MenuSound(AudioClip audioClip,float volume)
    {
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effects")[0];
        audioSource.clip = audioClip;
        audioSource.Play();
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(audioSource.clip.length+1);
        Destroy(gameObject);
    }
}