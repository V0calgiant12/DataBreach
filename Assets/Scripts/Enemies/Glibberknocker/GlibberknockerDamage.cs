using System.Collections;
using UnityEngine;

public class GlibberknockerDamage : MonoBehaviour
{
    [Header("Glibberknocker Damage References:")]
    public Rigidbody2D GlibberknockerRb;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerStateManager.Instance.DamagePlayer(Random.Range(10, 15), Random.Range(5, 10), 90, false, transform.position.x, false);
        }
    }
}
