using System.Collections;
using System.Numerics;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    private UnityEngine.Vector3 Velocity = UnityEngine.Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        UnityEngine.Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random point inside a sphere and multiply by magnitude
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = UnityEngine.Vector3.SmoothDamp(transform.position, originalLocalPosition + new UnityEngine.Vector3(x, y, -10), ref Velocity, magnitude);
            //transform.localPosition = originalLocalPosition + new Vector3(x, y, z);

            elapsed += 1; // We are not using Time.deltaTime in this project

            yield return null; // Wait until the next frame
        }

        // Return the camera to its original local position
        transform.localPosition = originalLocalPosition;
    }
}
