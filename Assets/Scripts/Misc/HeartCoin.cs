using UnityEngine;

public class HeartCoin : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private PlayerData playerData;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlaySound(audioClip,1,1,0,1,transform.position);
            playerData.hasHeartCoin = true;
            HeartCoinIconHandler.Instance.UpdateGUI();
            Destroy(gameObject);
        }
    }
}