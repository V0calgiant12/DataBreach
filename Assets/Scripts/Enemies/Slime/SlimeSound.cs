using UnityEngine;

public class SlimeSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void PlaySlimeJumpSound(AudioClip audio)
        {
            GameObject audioClone = Instantiate(prefab);
            audioClone.GetComponent<SlimeAudioSource>().SlimeJumpSound(audio,1f);
        }
}
