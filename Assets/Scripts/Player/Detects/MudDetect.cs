using UnityEngine;
using UnityEngine.Audio;
public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    public Rigidbody2D PlayerRb;
    public AudioClip MudWalk;
    
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
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud"))
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
        }
    }
}
