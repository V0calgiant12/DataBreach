using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public PlayerData PlayerDataRef;
    [SerializeField] private Rigidbody2D rb;
    public void AddForce(float xForce, float yForce, Collider2D other)
    {
        rb = other.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(rb.linearVelocityX, yForce + rb.linearVelocityY);
        if (rb.linearVelocityY <= 0 && other.gameObject.CompareTag("Player"))
        {
            PlayerDataRef.inAirGust = false;
        }
    }
}