using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public bool withinRange = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            withinRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            withinRange = false;
        }
    }
    
}