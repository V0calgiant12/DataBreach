using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void PlayJumpSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().JumpSound(audio,1f);
    }
    public void PlayGrassSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().GrassSound(audio,0.8f);
    }
    public void PlayStoneSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().StoneSound(audio,1f);
    }
}