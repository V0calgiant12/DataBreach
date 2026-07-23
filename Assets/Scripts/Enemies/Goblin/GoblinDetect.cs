using UnityEngine;

public class GoblinDetect : MonoBehaviour
{
    [SerializeField] private GoblinStateManager goblin;
    [SerializeField] private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && goblin.currentState != goblin.DeadState)
        {
            goblin.SwitchState(goblin.ChasingState);
            goblin.PlaySound(goblin.goblinAggro,1);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && goblin.currentState != goblin.DeadState)
        {
            goblin.SwitchState(goblin.IdleState);
            goblin.PlaySound(goblin.goblinDeaggro,1);
        }
    }
    
}