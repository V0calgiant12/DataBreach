using UnityEngine;

// Example of how to call the shake effect from another script
public class TriggerShake : MonoBehaviour
{
    public float shakeDuration = 30;
    public float shakeMagnitude = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Start the coroutine on the CameraShaker instance
            StartCoroutine(CameraShaker.Instance.Shake(shakeDuration, shakeMagnitude));
        }
    }
}
