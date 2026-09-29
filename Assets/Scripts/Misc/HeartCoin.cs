using System.Collections;
using UnityEngine;

public class HeartCoin : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator anim;
    private bool collected = false;
    void Start()
    {
        if (playerData.hasHeartCoin)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;
            audioSource.PlaySound(audioClip,1,1,0,1,transform.position);
            playerData.hasHeartCoin = true;
            HeartCoinIconHandler.Instance.UpdateGUI();
            anim.SetTrigger("Collect");
            StartCoroutine(Delete());
        }
    }
    private IEnumerator Delete()
    {
        float elapsed = 0;
        while(elapsed < 2)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}