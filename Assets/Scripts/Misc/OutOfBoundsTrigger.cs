using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStateManager.Instance.DamagePlayer(0,0,0,true,0,true);
            PlayerStateManager.Instance.playerData.playerHealth = 0;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}