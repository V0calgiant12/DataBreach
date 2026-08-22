using UnityEngine;

public class GoblinDetect : MonoBehaviour
{
    [SerializeField] private GoblinStateManager goblin;
    [SerializeField] private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goblin.aggro = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goblin.aggro = false;
        }
    }
    
}