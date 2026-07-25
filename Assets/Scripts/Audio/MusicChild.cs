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
    public void FadeOutCaller(int speed)
    {
        if (active)
        {
            StartCoroutine(FadeOut(speed));
        }
    }
    public void FadeInCaller(int speed)
    {
        StartCoroutine(FadeIn(speed));
    }
    public void Activate()
    {
        active = true;
        StartCoroutine(FadeIn(120));
    }
    public void Deactivate()
    {
        active = false;
        StartCoroutine(FadeOut(60));
    }

    
    private IEnumerator FadeOut(int speed)
    {
        float currentVol = audioSource.volume;
        while(audioSource.volume > 0)
        {
            audioSource.volume -= currentVol/speed;
            yield return null;
        }
    }
    private IEnumerator FadeIn(int speed)
    {
        if (active)
        {
            Debug.Log("fading in",this);
            float currentVol = audioSource.volume;
            while (audioSource.volume < maxVol)
            {
                audioSource.volume += (maxVol-currentVol)/speed;
                yield return null;
            }
        }
    }
}
