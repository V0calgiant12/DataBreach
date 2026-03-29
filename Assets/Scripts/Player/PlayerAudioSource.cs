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

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}