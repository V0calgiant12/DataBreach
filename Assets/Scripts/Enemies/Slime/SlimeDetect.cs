using UnityEngine;

public class SlimeDetect : MonoBehaviour
{
    [SerializeField] private SlimeStateManager slime;
    [SerializeField] private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            slime.currentState = SlimeStateManager.State.Chase;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            slime.currentState = SlimeStateManager.State.Idle;
        }
    }
    
}