using Unity.VisualScripting;
using UnityEngine;

public class TextTriggerArea : MonoBehaviour
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
            PlayerStateManager.Instance.Interact();
            TextWrite.Instance.WriteText(GetComponent<TextData>());

            if (CanRepeat)
            {
                Triggered = false;
            }
            if(DamagePlayer)
            {
                PlayerStateManager.Instance.DamagePlayer(0,0,60,true,transform.position.x,true);
            }
            if (HealPlayer)
            {
                audioSource.Play();
                PlayerStateManager.Instance.playerData.playerHealth += 1;
            }
        }
    }
}