using System.Collections;
using UnityEngine;

public class SlimeDamage : MonoBehaviour
{
    public Rigidbody2D slimeRb;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStateManager.Instance.DamagePlayer(Random.Range(10,15),Random.Range(5,10), 90, false);
        }
    }
}
