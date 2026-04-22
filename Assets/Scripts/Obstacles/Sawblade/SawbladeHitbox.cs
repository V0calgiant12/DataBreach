using UnityEngine;

public class SawbladeHitbox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player is hit by the hitbox, it will call a function that subtracts 1 health and launches the player
        if(other.gameObject.CompareTag("Player")) 
        {
            PlayerStateManager.Instance.DamagePlayer(10, Random.Range(6,10),60,false,transform.position.x,false);
        }
        // If an enemy is hit by the hitbox, it will call a function that subtracts 1 health and launches the enemy
        if(other.gameObject.CompareTag("EnemyHurtbox"))
        {
            Debug.Log(other.gameObject);
            other.gameObject.GetComponent<EnemyHit>().DamageEnemy(1, 15, Random.Range(8,10),transform.position.x);
        }
    }
}