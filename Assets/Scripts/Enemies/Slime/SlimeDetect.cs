using UnityEngine;

public class SlimeDetect : MonoBehaviour
{
    [SerializeField] private SlimeStateManager slime;
    [SerializeField] private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TEST");
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TriggerEnter");
            slime.currentState = SlimeStateManager.State.Chase;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TriggerExit");
            slime.currentState = SlimeStateManager.State.Idle;
        }
    }
    
}