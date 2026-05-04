using UnityEngine;

public class ForceManager : MonoBehaviour
{
    public PlayerData PlayerDataRef;
    [SerializeField] private Rigidbody2D rb;
    public void AddForce(float xForce, float yForce, Collider2D other)
    {
        Debug.Log("adding force");
        rb.linearVelocity = new Vector2(xForce, yForce + rb.linearVelocityY);
        if (rb.linearVelocityY <= 0)
        {
            PlayerDataRef.inAirGust = false;
        }
    }
}