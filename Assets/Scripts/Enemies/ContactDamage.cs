using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private bool StopAtOneHp = false;
    [SerializeField] private Vector2 knockback;
    private Vector2 appliedKnockback;
    [SerializeField] private bool rotationDependant = false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!StopAtOneHp || StopAtOneHp && PlayerStateManager.Instance.playerData.playerHealth != 1)
            {
                appliedKnockback = knockback;
                if (rotationDependant)
                {
                    //Debug.Log(transform.eulerAngles.z);
                    switch (transform.eulerAngles.z)
                    {
                        case (0):
                            // Nothing changes.
                            break;
                        case(90):
                            appliedKnockback = new Vector2(knockback.x*1.5f, knockback.y*0.5f);
                            break;
                        case(180):
                            appliedKnockback = new Vector2(knockback.x*0.5f, knockback.y*-1);
                            break;
                        case(270):
                            appliedKnockback = new Vector2(knockback.x*1.5f, knockback.y*0.5f);
                            break;
                    }
                }
                PlayerStateManager.Instance.DamagePlayer(appliedKnockback.x, appliedKnockback.y, 30, false, transform.position.x, false);
            }
        }
    }
}
