using UnityEngine;

// Example of how to call the shake effect from another script
public class TriggerShake : MonoBehaviour
{
    public int shakeDuration = 30;
    public float shakeMagnitude = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Start the coroutine on the CameraShaker instance
            StartCoroutine(CameraShaker.Instance.Shake(shakeDuration, shakeMagnitude));
        }
    }
}
