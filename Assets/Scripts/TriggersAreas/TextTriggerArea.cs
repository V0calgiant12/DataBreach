using Unity.VisualScripting;
using UnityEngine;

public class TextTriggerArea : MonoBehaviour
{
    [SerializeField] bool Triggered = false;
    [SerializeField] bool CanRepeat = false;
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
        }
    }
}