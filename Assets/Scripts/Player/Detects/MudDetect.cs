using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    public SlimeStateManager SlimeStateManagerRef;
    public GameObject Player;
    public GameObject Slime;
    public Rigidbody2D PlayerRb;
    
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 0.6f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 0.6f;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Slime"))
        {
            SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
            SlimeStateManagerRef.mudSpeedMulti = 0.6f;
            SlimeStateManagerRef.mudJumpMulti = 0.6f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Slime"))
        {
            SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
            SlimeStateManagerRef.mudSpeedMulti = 1f;
            SlimeStateManagerRef.mudJumpMulti = 1f;
        }
    }
}
