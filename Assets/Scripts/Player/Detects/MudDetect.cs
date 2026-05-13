using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    public GameObject Player;
    public GameObject Slime;
    public Rigidbody2D PlayerRb;
    
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && other.gameObject == Player)
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 0.6f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 0.6f;
        }
        if(other.gameObject.CompareTag("Mud") && other.gameObject == Slime)
        {
            //filler
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && other.gameObject == Player)
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
        }
    }
}
