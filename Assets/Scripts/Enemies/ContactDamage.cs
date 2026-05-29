using System.Collections;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private bool StopAtOneHp = false;
    [SerializeField] private Vector2 knockback;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!StopAtOneHp || StopAtOneHp && PlayerStateManager.Instance.playerData.playerHealth != 1)
            {
                PlayerStateManager.Instance.DamagePlayer(knockback.x, knockback.y, 30, false, transform.position.x, false);
            }
        }
    }
}
