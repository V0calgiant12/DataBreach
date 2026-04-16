using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [Header("Effect Sound References:")]
    [SerializeField] private GameObject prefab;
    public void HeartSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<EffectAudioSource>().HeartSound(audio,0.9f);
    }
}