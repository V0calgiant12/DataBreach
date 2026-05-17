using UnityEngine;
/// <summary>
/// This handels enemies taking damage. They will automatically take damage from a player's attack hitbox.
/// Damage can also be called on an enemy via a function.
/// </summary>
public class EnemyHit : MonoBehaviour
{
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
    private int trackedHealth = 1;
    private int iFrames = 0;
    public bool knockbackImmune = false;
    public bool invulnerable = false;
    private void Start()
    {
        trackedHealth = health;
        if(flashEffect == null)
        {
            flashEffect = ParentObject.GetComponent<FlashEffect>();
        }
    }
    private void Update() 
    {
        iFrames -= 1;
        if (trackedHealth <= 0)
        {
            audioSource.EnemySound(deathSound,volume);
            Instantiate(particlePrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(ParentObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox") && iFrames < 0)
        {
            iFrames = 15;
            TriggerShake.Instance.BurstShake(1,2);
            if (!invulnerable)
            {
                DamageEnemy(1,10,10,other.transform.position.x);
            }
            else
            {
                audioSource.EnemySound(hitSound,volume);
                flashEffect.WhiteFlash();
                Instantiate(particlePrefab, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
    public void DamageEnemy(int damage, float xLaunch, float yLaunch, float damageSourceX)
    {
        //Debug.Log("Damaged Enemy for " + damage + " damage.");
        if(trackedHealth != 1)
        {
            audioSource.EnemySound(hitSound,volume);
            flashEffect.WhiteFlash();
        }
        if (!knockbackImmune)
        {
            rb.linearVelocity = new Vector2(xLaunch*(transform.position.x <= damageSourceX ? -1 : 1), yLaunch);
        }
        trackedHealth -= damage;
    }
}