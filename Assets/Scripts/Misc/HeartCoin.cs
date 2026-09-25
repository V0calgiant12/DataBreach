using UnityEngine;

public class HeartCoin : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip audioClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.PlaySound(audioClip,1,1,0,1,transform.position);
        Destroy(gameObject);
    }
}