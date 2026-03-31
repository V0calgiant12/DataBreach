using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void HeartSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<EffectAudioSource>().HeartSound(audio,0.9f);
    }
}