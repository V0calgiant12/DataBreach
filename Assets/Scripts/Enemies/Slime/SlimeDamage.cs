using System.Collections;
using UnityEngine;

public class SlimeDamage : MonoBehaviour
{
    [Header("Slime Damage References:")]
    public Rigidbody2D slimeRb;
    public AudioSource audioSource3;
    [SerializeField] private bool enemyDamagedPlayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource3.Play();
            enemyDamagedPlayer = true;
            PlayerStateManager.Instance.DamagePlayer(Random.Range(10,15),Random.Range(5,10), 90, false, transform.position.x, false);
        }
        else
        {
            enemyDamagedPlayer = false;
        }
    }
}
