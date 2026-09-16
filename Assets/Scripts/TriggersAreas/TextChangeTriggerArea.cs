using Unity.VisualScripting;
using UnityEngine;

public class TextChangeTriggerArea : MonoBehaviour
{
    [SerializeField] private bool Triggered = false;
    [SerializeField] private string[] newText;
    [Tooltip("Will replace the text of the Text Data inputed here. If not Text Data is inputed, it will default to attempting to find one in this game object.")]
    [SerializeField] private TextData dataOverride;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !Triggered)
        {
            Triggered = true;
            if (GetComponent<TextData>() != null && dataOverride != null)
            {
                GetComponent<TextData>()._TextPageInput = newText;
            }
            else if(dataOverride != null)
            {
                dataOverride._TextPageInput = newText;
            }
            else
            {
                Debug.LogError("ERROR: No Text Data component found.",this);
            }
        }
    }
}