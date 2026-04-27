using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    public Rigidbody2D PlayerRb;
    
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud"))
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 0.6f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 0.6f;
        }
        else
        {
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
        }
    }
}
