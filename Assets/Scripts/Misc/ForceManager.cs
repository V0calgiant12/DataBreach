using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public void AddForce(float xForce, float yForce, Collider2D thisObject)
    {
        
        rb.linearVelocity = new Vector2(xForce + rb.linearVelocityX, yForce + rb.linearVelocityY);
        
        if (rb.linearVelocityY <= 0 && thisObject.gameObject.CompareTag("Player"))
        {
            PlayerStateManager.Instance.playerData.inAirGust = false;
        }
    }
}