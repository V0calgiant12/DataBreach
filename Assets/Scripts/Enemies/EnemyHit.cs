using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// This handels enemies taking damage. They will automatically take damage from a player's attack hitbox.
/// Damage can also be called on an enemy via a function.
/// </summary>
public class EnemyHit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ParentObject;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private FlashEffect flashEffect;

    [Header("Audio")]
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float volume = 1;

    [Header("Stats")]
    [Tooltip("Default health values: Slime 3, Para-Slimes 1, Goblin 5, Gliberknocker 6")]
    public int health = 1;
    [SerializeField] private Vector2 knockbackReduction = new Vector2(1,1);
    public bool knockbackImmune = false;
    public bool invulnerable = false;
    public bool immediatelDestroyOnDeath = true;

    [Header("Stored Info")]
    [SerializeField] private int iFrames = 0;
    public int trackedHealth = 1;
    public bool _DamageTaken = false;
    public Vector2 _LastKnockbackTaken;
    [SerializeField] private GameObject[] sprites;
    
    private void Start()
    {
        if(knockbackReduction.x == 0)
        {
            knockbackReduction.x = 1;
        }
        if(knockbackReduction.y == 0)
        {
            knockbackReduction.y = 1;
        }
        trackedHealth = health;
        if(flashEffect == null)
        {
            flashEffect = ParentObject.GetComponent<FlashEffect>();
        }
        
    }
    private void Update() 
    {
        iFrames -= (Time.timeScale == 1) ? 1 : 0;
        if (iFrames <= 0)
        {
            _DamageTaken = false;
        }
        if (trackedHealth <= 0 && immediatelDestroyOnDeath) // IF immediatelyDestroyOnDeath is false, we assume it's handeled elsewhere as it's likely a State Machine handeling it.
        {
            audioSource.PlaySound(deathSound,volume,1,1);
            if(particlePrefab != null)
            {
                Instantiate(particlePrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(ParentObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox") && iFrames < 0 && trackedHealth > 0)
        {
            iFrames = 15;
            _DamageTaken = true;
            TriggerShake.Instance.BurstShake(1,2,false);
            if (!invulnerable)
            {
                switch (PlayerStateManager.Instance.playerData.anim.GetInteger("attackId"))
                {
                    case(0):
                        // Forward attacks (0)
                        DamageEnemy(1,10,10,PlayerStateManager.Instance.transform.position.x);
                        break;
                    case(1):
                        // Up attacks (1)
                        DamageEnemy(1,2,30,PlayerStateManager.Instance.transform.position.x);
                        break;
                    case(2):
                        // Backward attacks (2)
                        DamageEnemy(1,7,10,PlayerStateManager.Instance.transform.position.x);
                        break;
                    case(3):
                        // Down attacks (3)
                        DamageEnemy(1,8,5,PlayerStateManager.Instance.transform.position.x);
                        break;
                    case(4):
                        // Down air attacks (4)
                        DamageEnemy(1,8,5,PlayerStateManager.Instance.transform.position.x);
                        break;
                    case(5):
                        // Dash attacks (5)
                        DamageEnemy(1,15,8,PlayerStateManager.Instance.transform.position.x);
                        break;
                }
            }
            else
            {
                audioSource.PlaySound(hitSound,volume,1,1);
                flashEffect.WhiteFlash();
                if(particlePrefab != null)
                {
                    Instantiate(particlePrefab, GameObject.Find("HitPoint").transform.position, gameObject.transform.rotation);
                }
            }
        }
    }
    public void DamageEnemy(int damage, float xLaunch, float yLaunch, float damageSourceX)
    {
        //Debug.Log("Damaged Enemy for " + damage + " damage.");
        if(trackedHealth != 1)
        {
            audioSource.PlaySound(hitSound,volume,1,1);
            AdvancedFlash(1);
        }
        if (!knockbackImmune)
        {
            _LastKnockbackTaken = new Vector2(xLaunch*(transform.position.x <= damageSourceX ? -1 : 1)/knockbackReduction.x, yLaunch/knockbackReduction.y);
            rb.linearVelocity = _LastKnockbackTaken;
        }
        trackedHealth -= damage;
    }
    
    public void AdvancedFlash(int type)
    {
        int index = 0;
        if(type == 1) // White Flash
        {
            Debug.Log("White Flash");
            while (index <= sprites.Length - 1) // Repeats for every game object.
            {
                sprites[index].SendMessage("WhiteFlash");
                index += 1;
            }
        }
        else if(type == 2) // Invulnerable Flash
        {
            Debug.Log("Invulnerable Flash");
            while (index <= sprites.Length - 1) // Repeats for every game object.
            {
                sprites[index].SendMessage("InvulnerableFlash", iFrames);
                index += 1;
            }
        }
    }
}