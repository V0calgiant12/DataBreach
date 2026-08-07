using UnityEngine;

public class MenuSound : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public void PlaySound(AudioClip audio)
    {
        if(SettingsData.Instance.loadDelay < 0)
        {
            GameObject audioClone = Instantiate(prefab);
            audioClone.GetComponent<MenuAudioSource>().MenuSound(audio,1);
        }
    }
}