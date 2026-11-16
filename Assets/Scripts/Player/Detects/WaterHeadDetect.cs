using UnityEngine;

public class WaterHeadDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            AudioEffectsController.LowpassEffect = true;
            AudioEffectsController.Instance.UpdateEffects();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            AudioEffectsController.LowpassEffect = false;
            AudioEffectsController.Instance.UpdateEffects();
        }
    }
}
