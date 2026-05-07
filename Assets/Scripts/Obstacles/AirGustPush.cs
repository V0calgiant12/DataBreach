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
        if (other.gameObject.CompareTag("Player") && !GroundCheckRef._IsGrounded)
        {
            if (!Input.GetKey(SettingsData.Instance._InputDown))
            {
                PlayerDataRef.inAirGust = true;
                other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength, other);
            }
        }
        if (other.gameObject.CompareTag("Slime"))
        {
            other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength, other);
        }
    }
}
