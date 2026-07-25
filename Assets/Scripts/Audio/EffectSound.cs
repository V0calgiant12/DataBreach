using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [Header("Effect Sound References:")]
    [SerializeField] private GameObject prefab;
    public void HeartSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab,transform.position,transform.rotation);
        audioClone.GetComponent<EffectAudioSource>().HeartSound(audio,0.9f);
    }
    public void PlaySound(AudioClip audio, float volume, float pitch, float spatialBlend)
    {
        GameObject audioClone = Instantiate(prefab,transform.position,transform.rotation);
        audioClone.GetComponent<EffectAudioSource>().PlaySound(audio, volume, pitch,spatialBlend);
    }
    public void PlaySlimeJumpSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab, transform.position,transform.rotation);
        audioClone.GetComponent<EffectAudioSource>().SlimeJumpSound(audio,0.8f);
    }
    public void PlayMudSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab,transform.position,transform.rotation);
        audioClone.GetComponent<EffectAudioSource>().MudSound(audio,1f);
    }
}