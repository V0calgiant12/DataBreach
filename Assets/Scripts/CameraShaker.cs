using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random point inside a sphere and multiply by magnitude
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalLocalPosition + new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        // Return the camera to its original local position
        transform.localPosition = originalLocalPosition;
    }
}
