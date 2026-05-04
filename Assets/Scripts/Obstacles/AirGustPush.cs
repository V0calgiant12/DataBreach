using UnityEngine;

public class AirGustPush : MonoBehaviour
{
    public float GustStrength;
    public GroundCheck GroundCheckRef;
    public PlayerData PlayerDataRef;
    void Start()
    {
        PlayerDataRef.inAirGust = false;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        PlayerDataRef.inAirGust = true;
        GroundCheckRef = other.gameObject.GetComponent<GroundCheck>();
        Debug.Log("test", other.gameObject);
        if (other.gameObject.CompareTag("Player") && !GroundCheckRef._IsGrounded)
        {
            Debug.Log("test");
            Debug.Log(other.gameObject.GetComponent<ForceManager>());
            other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength, other);
        }
    }
}
