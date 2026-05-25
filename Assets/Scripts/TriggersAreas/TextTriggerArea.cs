using Unity.VisualScripting;
using UnityEngine;

public class TextTriggerArea : MonoBehaviour
{
    [SerializeField] private bool Triggered = false;
    [SerializeField] private bool CanRepeat = false;
    [SerializeField] private bool DamagePlayer = false;
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
        }
    }
}