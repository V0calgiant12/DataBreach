using UnityEngine;

public class WaterDetect : MonoBehaviour
{
    [Header("Water References:")]
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip waterLand;
    [SerializeField] private AudioClip waterJump;
    private Rigidbody2D rb;
    private float baseGravity;
    private float baseLinearDamp;
    private float baseAngularDamp;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
        baseLinearDamp = rb.linearDamping;
        baseAngularDamp = rb.angularDamping;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Player"))
        {
            SpawnSplashParticles(other.gameObject);
            audioSource.PlaySound(waterLand,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
            gameObject.GetComponent<PlayerStateManager>().playerData.inLiquid = true;
        }
        if(other.gameObject.CompareTag("Water") && gameObject.CompareTag("Enemy"))
        {
            SpawnSplashParticles(other.gameObject);
            audioSource.PlaySound(waterLand,0.8f,Random.Range(0.8f,1.2f),1,1,transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Water"))
        {
            rb.gravityScale = baseGravity/2f;
            rb.linearDamping = 2f;
            rb.angularDamping = 2f;
            // Limit decent in water
            if(rb.linearVelocityY < -5)
            {
                rb.linearVelocityY = -5f;
            }
            if (gameObject.CompareTag("Player"))
            {
                PlayerStateManager.Instance.playerData.waterSpeedMulti = 0.25f;
                PlayerStateManager.Instance.playerData.waterJumpMulti = 1f;
            }
            if (gameObject.CompareTag("Enemy"))
            {
                switch(gameObject.name)
                {
                    case("Slime"):
                        SlimeStateManager SlimeStateManagerRef;
                        SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                        SlimeStateManagerRef.waterSpeedMulti = 2f;
                        SlimeStateManagerRef.waterJumpMulti = 1f;
                        SlimeStateManagerRef.floating = true;
                        gameObject.GetComponent<ForceManager>().AddForce(0f, 1.5f * (Time.timeScale == 1 ? 1 : 0), other);
                        break;
                    case("Goblin"):
                        GoblinStateManager goblinStateManager;
                        goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                        goblinStateManager.waterSpeedMulti = 0.5f;
                        goblinStateManager.waterJumpMulti = 1f;
                        break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            SpawnSplashParticles(other.gameObject);
            rb.gravityScale = baseGravity;
            rb.linearDamping = baseLinearDamp;
            rb.angularDamping = baseAngularDamp;
            if (gameObject.CompareTag("Player"))
            {
                audioSource.PlaySound(waterJump,1f,Random.Range(0.8f,1.2f),0,1,transform.position);
                PlayerStateManager.Instance.playerData.waterSpeedMulti = 1f;
                PlayerStateManager.Instance.playerData.waterJumpMulti = 1f;
                gameObject.GetComponent<PlayerStateManager>().playerData.inLiquid = false;
            }
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Is enemy");
                audioSource.PlaySound(waterJump,0.5f,Random.Range(0.8f,1.2f),1,1,transform.position);
                switch(gameObject.name)
                {
                    case("Slime"):
                        SlimeStateManager SlimeStateManagerRef;
                        SlimeStateManagerRef = gameObject.GetComponent<SlimeStateManager>();
                        SlimeStateManagerRef.waterSpeedMulti = 1f;
                        SlimeStateManagerRef.waterJumpMulti = 1f;
                        SlimeStateManagerRef.floating = false;
                        return;
                    case("Goblin"):
                        GoblinStateManager goblinStateManager;
                        goblinStateManager = gameObject.GetComponent<GoblinStateManager>();
                        goblinStateManager.waterSpeedMulti = 1f;
                        goblinStateManager.waterJumpMulti = 1f;
                        return;
                }
            }
        }
    }
    private void SpawnSplashParticles(GameObject other)
    {
        ParticleSpawner ps = other.GetComponent<ParticleSpawner>();
        RaycastHit2D down = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f),Vector2.down,2.5f,LayerMask.GetMask("Water"));
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f),Vector2.down*2.5f,Color.skyBlue);
        ps.SpawnParticle("WaterSplash",new Vector2(transform.position.x, Mathf.Round(down.point.y)));
        ps.SpawnParticle("BigSplash",new Vector2(transform.position.x, Mathf.Round(down.point.y)+0.6f));
    }
}
