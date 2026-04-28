using UnityEngine;

public class AirGustPush : MonoBehaviour
{
    public GroundCheck GroundCheckRef;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !GroundCheckRef._IsGrounded)
        {
            Debug.Log("it works btw");
        }
    }
}
