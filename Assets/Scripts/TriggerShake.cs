using UnityEngine;

// Example of how to call the shake effect from another script
public class TriggerShake : MonoBehaviour
{
    public CameraShaker cameraShaker;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Start the coroutine on the CameraShaker instance
            StartCoroutine(cameraShaker.Shake(shakeDuration, shakeMagnitude));
        }
    }
}

