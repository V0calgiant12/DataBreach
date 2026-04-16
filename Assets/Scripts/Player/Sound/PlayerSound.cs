using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Player Sound References:")]
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
    public void PlayPlayerHitSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().PlayerHitSound(audio,1f);
    }
    public void PlayPlayerDeathSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().PlayerDeathSound(audio,1f);
    }
    //public void PlayPlayerDeathSound2(AudioClip audio)
    //{
        //GameObject audioClone = Instantiate(prefab);
        //audioClone.GetComponent<PlayerAudioSource>().PlayerDeathSound2(audio,1f);
    //}
    public void PlayPlayerAttackSound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<PlayerAudioSource>().PlayerAttackSound(audio,1f);
    }
}