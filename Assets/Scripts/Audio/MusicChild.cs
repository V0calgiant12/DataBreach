using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicChild : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float maxVol;
    public bool active = true;
    private void Awake()
    {
        maxVol = audioSource.volume;
        audioSource.volume = 0;
    }
    public void FadeOutCaller(float time)
    {
        if (active)
        {
            StartCoroutine(FadeOut(time));
        }
    }
    public void FadeInCaller(float time)
    {
        StartCoroutine(FadeIn(time));
    }
    public void Activate()
    {
        active = true;
        StartCoroutine(FadeIn(2));
    }
    public void Deactivate()
    {
        active = false;
        StartCoroutine(FadeOut(1));
    }

    
    private IEnumerator FadeOut(float time)
    {
        float currentVol = audioSource.volume;
        while(audioSource.volume > 0)
        {
            audioSource.volume -= currentVol/(time*Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator FadeIn(float time)
    {
        if (active)
        {
            float currentVol = audioSource.volume;
            while (audioSource.volume < maxVol)
            {
                audioSource.volume += (maxVol-currentVol)/(time*Time.deltaTime);
                yield return null;
            }
        }
    }
}
