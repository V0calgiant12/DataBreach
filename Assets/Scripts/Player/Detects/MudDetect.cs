using UnityEngine;

public class MudDetect : MonoBehaviour
{
    [Header("Mud References:")]
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip mudLand;
    [SerializeField] private AudioClip mudJump;
    
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Player"))
        {
            audioSource.PlaySound(mudLand,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
            gameObject.GetComponent<PlayerStateManager>().playerData.inMud = true;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Enemy"))
        {
            audioSource.PlaySound(mudLand,1f,Random.Range(0.8f,1.2f),1,1,transform.position);
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
            audioSource.PlaySound(mudJump,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
            PlayerStateManager.Instance.playerData.mudSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.mudJumpMulti = 1f;
            gameObject.GetComponent<PlayerStateManager>().playerData.inMud = false;
        }
        if(other.gameObject.CompareTag("Mud") && gameObject.CompareTag("Enemy"))
        {
            audioSource.PlaySound(mudJump,1f,Random.Range(0.8f,1.2f),1,1,transform.position);
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
