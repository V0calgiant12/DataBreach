using Unity.VisualScripting;
using UnityEngine;

public class HealthTriggerArea : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool Triggered = false;
    [SerializeField] private bool CanRepeat = false;
    [SerializeField] private bool DamagePlayer = false;
    [SerializeField] private bool HealPlayer = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !Triggered)
        {
            Triggered = true;
            if (CanRepeat)
            {
                Triggered = false;
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