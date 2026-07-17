using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    [SerializeField] private PlayerSound playerAudioSource;
    [SerializeField] private EffectSound otherAudioSource;
    [SerializeField] private AudioClip mudLand;
    [SerializeField] private AudioClip mudJump;
    
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {
            playerAudioSource.PlayMudSound(mudLand);
            gameObject.GetComponent<PlayerStateManager>().playerData.inMud = true;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Enemy"))
        {
            otherAudioSource.PlayMudSound(mudLand);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {   
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 0.5f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 0.6f;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Enemy"))
        {
            switch(gameObject.name)
            {
                case("Slime"):
                    SlimeStateManager SlimeStateManagerRef;
                    SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                    SlimeStateManagerRef.mudSpeedMulti = 0.6f;
                    SlimeStateManagerRef.mudJumpMulti = 0.6f;
                    break;
                case("Goblin"):
                    GoblinStateManager goblinStateManager;
                    goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                    goblinStateManager.mudSpeedMulti = 0.7f;
                    goblinStateManager.mudJumpMulti = 0.75f;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {   
            playerAudioSource.PlayMudSound(mudJump);
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
            gameObject.GetComponent<PlayerStateManager>().playerData.inMud = false;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Enemy"))
        {
            otherAudioSource.PlayMudSound(mudJump);
            switch(gameObject.name)
            {
                case("Slime"):
                    SlimeStateManager SlimeStateManagerRef;
                    SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                    SlimeStateManagerRef.mudSpeedMulti = 1f;
                    SlimeStateManagerRef.mudJumpMulti = 1f;
                    break;
                case("Goblin"):
                    GoblinStateManager goblinStateManager;
                    goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                    goblinStateManager.mudSpeedMulti = 1f;
                    goblinStateManager.mudJumpMulti = 1f;
                    break;
            }
        }
    }
}
