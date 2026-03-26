using System.Collections;
using System.Numerics;
using UnityEngine;

/// <summary>
/// This script is put in the Camera Location game object on the player.
/// A camera shake can be activated from anywhere in any script using `StartCoroutine(CameraShaker.Instance.Shake(<int duration>, <float magnitude>));`.
/// This script will then activate and begin shaking whatever object this script is attached to.
/// Duration is in frames, magnitude has no measure associated with it.
/// </summary>
public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    private UnityEngine.Vector3 Velocity = UnityEngine.Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake(int duration, float magnitude) // Duration is in frames.
    {
        UnityEngine.Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random point inside a sphere and multiply by magnitude
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalLocalPosition + new UnityEngine.Vector3(x, y, -10);

            elapsed += 1; // We are not using Time.deltaTime in this project, so this counts up every frame.

            yield return null; // Wait until the next frame
        }

        // Return the camera to its original local position after while loop is done.
        transform.localPosition = originalLocalPosition;
    }
}
