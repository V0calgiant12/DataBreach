using Unity.VisualScripting;
using UnityEngine;

public class TextTriggerArea : MonoBehaviour
{
    [SerializeField] private bool Triggered = false;
    [SerializeField] private bool CanRepeat = false;
    [SerializeField] private bool HeartCoinReminder = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if((HeartCoinReminder && PlayerStateManager.Instance.playerData.hasHeartCoin) || !HeartCoinReminder)
        {
            if (other.gameObject.CompareTag("Player") && !Triggered)
            {
                Triggered = true;
                if (GetComponent<TextData>() != null)
                {
                    PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.Stop,0);
                    TextWrite.Instance.WriteText(GetComponent<TextData>());
                }

                if (CanRepeat)
                {
                    Triggered = false;
                }
            }
        }
    }
}