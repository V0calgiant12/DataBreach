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
            if (UserInput.Instance.MovementInput.y > -0.5f)
            {
                PlayerDataRef.inAirGust = true;
                other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength * (Time.timeScale == 1 ? 1 : 0), other);
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyGroundCheck enemyGroundCheck = other.gameObject.GetComponentInChildren<EnemyGroundCheck>();
            if (enemyGroundCheck != null && !enemyGroundCheck._IsGrounded)
            {
                other.gameObject.GetComponent<ForceManager>().AddForce(0f, GustStrength * (Time.timeScale == 1 ? 1 : 0), other);
            }
        }
    }
}
