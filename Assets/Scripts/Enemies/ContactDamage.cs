using System.Collections;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public AudioSource audioSource3;
    [SerializeField] private bool StopAtOneHp = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!StopAtOneHp || StopAtOneHp && PlayerStateManager.Instance.playerData.playerHealth != 1)
            {
                audioSource3.Play();
                PlayerStateManager.Instance.DamagePlayer(Random.Range(10,15),Random.Range(5,10), 90, false, transform.position.x, false);
            }
        }
    }
}
