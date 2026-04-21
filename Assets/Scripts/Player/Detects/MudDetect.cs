using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    public Rigidbody2D PlayerRb;
    public float mudSpeedMulti;
    public float mudJumpMulti;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud"))
        {   
            mudSpeedMulti = 0.5f;
        }
        else
        {
            mudSpeedMulti = 1f;
        }
    }
}
