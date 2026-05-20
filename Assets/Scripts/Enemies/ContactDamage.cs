using System.Collections;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [Header("References:")]
    public AudioSource audioSource3;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource3.Play();
            PlayerStateManager.Instance.DamagePlayer(Random.Range(10,15),Random.Range(5,10), 90, false, transform.position.x, false);
        }
    }
}
