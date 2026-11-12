using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyHurtbox"))
        {
            Debug.Log("Enemy Hit by Barrier");
            EnemyHit enemyHit = other.gameObject.GetComponent<EnemyHit>();
            Transform enemyTransform = enemyHit.ParentObject.transform;
            enemyTransform.position = new Vector2(enemyTransform.position.x,enemyTransform.position.y + 0.2f);
            enemyHit.DamageEnemy(1,40,30,transform.position.x);
        }
    }
}