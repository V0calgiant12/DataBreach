using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HealthTriggerArea : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool Triggered = false;
    [SerializeField] private bool CanRepeat = false;
    [SerializeField] private bool DamagePlayer = false;
    [SerializeField] private bool HealPlayer = false;
    [SerializeField] private SpriteRenderer LaserTop;
    [SerializeField] private SpriteRenderer LaserBottom;
    [SerializeField] private Light2D LaserLight;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject HealthParticle;
    [SerializeField] private GameObject DamageParticle;
    [SerializeField] private Color laserColor;
    [SerializeField] private Color lightColor;
    void Start()
    {
        if(DamagePlayer)
        {
            laserColor = UnityEngine.Color.HSVToRGB(0,0.5f,1);
            lightColor = UnityEngine.Color.HSVToRGB(0,1f,1);
            LaserLight.intensity = 1;
            DamageParticle.SetActive(true);
        }
        else if (HealPlayer)
        {
            laserColor = UnityEngine.Color.HSVToRGB(0.4f,0.5f,1);
            lightColor = UnityEngine.Color.HSVToRGB(0.4f,1,1);
            LaserLight.intensity = 1;
            HealthParticle.SetActive(true);
        }
        else
        {
            laserColor = UnityEngine.Color.HSVToRGB(0,0,1);
            lightColor = UnityEngine.Color.HSVToRGB(0,0,1);
            LaserLight.intensity = 1;
        }
        LaserTop.color = new UnityEngine.Color(laserColor.r, laserColor.g, laserColor.b, laserColor.a);
        LaserBottom.color = new UnityEngine.Color(laserColor.r, laserColor.g, laserColor.b, laserColor.a);
        LaserLight.color = new UnityEngine.Color(lightColor.r, lightColor.g, lightColor.b, lightColor.a);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !Triggered)
        {
            Triggered = true;
            if (CanRepeat)
            {
                Triggered = false;
            }
            else
            {
                anim.SetTrigger("TurnOff");
                HealthParticle.SetActive(false);
                DamageParticle.SetActive(false);
            }
            if(DamagePlayer)
            {
                PlayerStateManager.Instance.DamagePlayer(0,0,60,true,transform.position.x,true);
            }
            else if (HealPlayer)
            {
                if(PlayerStateManager.Instance.playerData.playerHealth != PlayerStateManager.Instance.playerData.maxHealth)
                {
                    audioSource.Play();
                }
                PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.maxHealth;
            }
            else
            {
                Debug.LogError("ERROR: Health Trigger Area not set to heal or damage the player. Please select one in the Inpsector.", this);
            }
        }
    }
}