using Unity.VisualScripting;
using UnityEngine;

public class HealthTriggerArea : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool Triggered = false;
    [SerializeField] private bool CanRepeat = false;
    [SerializeField] private bool DamagePlayer = false;
    [SerializeField] private bool HealPlayer = false;
    [SerializeField] private SpriteRenderer LaserTop;
    [SerializeField] private SpriteRenderer LaserBottom;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject HealthParticle;
    [SerializeField] private GameObject DamageParticle;
    [SerializeField] private Color laserColor;
    void Start()
    {
        if(DamagePlayer)
        {
            laserColor = UnityEngine.Color.HSVToRGB(0,0.5f,1);
            DamageParticle.SetActive(true);
        }
        else if (HealPlayer)
        {
            laserColor = UnityEngine.Color.HSVToRGB(0.4f,0.5f,1);
            HealthParticle.SetActive(true);
        }
        else
        {
            laserColor = UnityEngine.Color.HSVToRGB(0,0,1);
        }
        LaserTop.color = new UnityEngine.Color(laserColor.r, laserColor.g, laserColor.b, laserColor.a);
        LaserBottom.color = new UnityEngine.Color(laserColor.r, laserColor.g, laserColor.b, laserColor.a);
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
                if(PlayerStateManager.Instance.playerData.playerHealth != 5)
                {
                    audioSource.Play();
                }
                PlayerStateManager.Instance.playerData.playerHealth = 5;
            }
            else
            {
                Debug.LogError("ERROR: Health Trigger Area not set to heal or damage the player. Please select one in the Inpsector.", this);
            }
        }
    }
}