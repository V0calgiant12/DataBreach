using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void PlaySound(AudioClip audio)
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<MenuAudioSource>().MenuSound(audio);
    }
}