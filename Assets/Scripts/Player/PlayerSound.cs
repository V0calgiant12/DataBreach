using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void PlayGrassSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().GrassSound(audio,0.7f);
    }
}