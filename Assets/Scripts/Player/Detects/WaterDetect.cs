using UnityEngine;

public class WaterDetect : MonoBehaviour
{
    [Header("Water References:")]
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip waterLand;
    [SerializeField] private AudioClip waterJump;
    public Rigidbody2D rb;
    public float baseGravity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Player"))
        {
            audioSource.PlaySound(waterLand,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
            gameObject.GetComponent<PlayerStateManager>().playerData.inLiquid = true;
        }
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Enemy"))
        {
            audioSource.PlaySound(waterLand,1f,Random.Range(0.8f,1.2f),1,1,transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Player"))
        {
            rb.gravityScale = baseGravity/1.5f;
            PlayerStateManager.Instance.playerData.waterSpeedMulti = 0.5f;
            //PlayerStateManager.Instance.playerData.waterJumpMulti = 0.6f;
        }
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Enemy"))
        {
            rb.gravityScale = baseGravity/1.5f;
            switch(gameObject.name)
            {
                case("Slime"):
                    SlimeStateManager SlimeStateManagerRef;
                    SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                    SlimeStateManagerRef.waterSpeedMulti = 0.6f;
                    SlimeStateManagerRef.waterJumpMulti = 0.6f;
                    break;
                case("Goblin"):
                    GoblinStateManager goblinStateManager;
                    goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                    goblinStateManager.waterSpeedMulti = 0.7f;
                    goblinStateManager.waterJumpMulti = 0.75f;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Player"))
        {
            rb.gravityScale = baseGravity;
            audioSource.PlaySound(waterJump,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
            PlayerStateManager.Instance.playerData.waterSpeedMulti = 1f;
            PlayerStateManager.Instance.playerData.waterJumpMulti = 1f;
            gameObject.GetComponent<PlayerStateManager>().playerData.inLiquid = false;
        }
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Enemy"))
        {
            rb.gravityScale = baseGravity;
            audioSource.PlaySound(waterJump,1f,Random.Range(0.8f,1.2f),1,1,transform.position);
            switch(gameObject.name)
            {
                case("Slime"):
                    SlimeStateManager SlimeStateManagerRef;
                    SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                    SlimeStateManagerRef.waterSpeedMulti = 1f;
                    SlimeStateManagerRef.waterJumpMulti = 1f;
                    break;
                case("Goblin"):
                    GoblinStateManager goblinStateManager;
                    goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                    goblinStateManager.waterSpeedMulti = 1f;
                    goblinStateManager.waterJumpMulti = 1f;
                    break;
            }
        }
    }
}
