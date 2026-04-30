using UnityEngine;

public class AirGustPush : MonoBehaviour
{
    public GroundCheck GroundCheckRef;
    public GameObject Player;
    public Rigidbody2D PlayerRb;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !GroundCheckRef._IsGrounded)
        {
            //filler bc I gotta go
        }
    }
}
