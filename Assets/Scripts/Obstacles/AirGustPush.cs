using UnityEngine;

public class AirGustPush : MonoBehaviour
{
    public float GustStrength;
    public GroundCheck GroundCheckRef;
    public SlimeStateManager SlimeStateManagerRef;
    public PlayerData PlayerDataRef;
    void Start()
    {
        PlayerDataRef.inAirGust = false;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        PlayerDataRef.inAirGust = true;
        Debug.Log("test", other.gameObject);
        if (other.gameObject.CompareTag("Player") && !GroundCheckRef._IsGrounded)
        {
            other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength, other);
        }
        if (other.gameObject.CompareTag("Slime") && !SlimeStateManagerRef.isGrounded)
        {
            other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength, other);
        }
    }
}
