using System.Collections;
using System.Numerics;
using UnityEngine;

/// <summary>
/// This script is put in the Camera Location game object on the player.
/// A camera shake can be activated from anywhere in any script using `StartCoroutine(CameraShaker.Instance.Shake(<int duration>, <float magnitude>));`.
/// This script will then activate and begin shaking whatever object this script is attached to.
/// Duration is in frames, magnitude has no measure associated with it.
/// 
/// If it is not a monobehavior script, use TriggerShake.Instance.Shake(<int duration>, <float magnitude>)
/// Same thing with an extra step.
/// </summary>
public class CameraShaker : MonoBehaviour
{
    [Header("Camera Shaker References:")]
    [SerializeField] private AudioSource audioSource;
    private UnityEngine.Vector3 Velocity = UnityEngine.Vector3.zero;
    public static CameraShaker Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Shake(int duration, float magnitude) // Duration is in frames.
    {
        Debug.Log("Camera shake " + duration + " " + magnitude);
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

    public IEnumerator BurstShake(float magnitude, float lengthMult, bool playSound, float overrideVolume)
    {
        Debug.Log("Camera Burst Shake " + magnitude + ", pitch: " + (2.5f - (magnitude * 0.09f * (magnitude * 0.09f))));
        UnityEngine.Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0f;

        if (playSound)
        {
            audioSource.pitch = 2.5f - ((magnitude * 0.09f * (magnitude * 0.09f)) + Random.Range(-0.1f, 0.1f)); // p = 2.5 - (0.09 * m)^2
            audioSource.volume = overrideVolume == 0 ? magnitude * 0.04f : overrideVolume;
            audioSource.Play();
        }

        while (elapsed < 1 + Mathf.Round(0.2f*magnitude*lengthMult*(0.15f*magnitude*lengthMult))) // l = (0.2m*L)^2
        {
            // Generate a random point inside a sphere and multiply by magnitude
            float x = Random.Range(-0.8f, 0.8f) * magnitude;
            float y = Random.Range(-1.5f, 1f) * magnitude;


            transform.localPosition = originalLocalPosition + new UnityEngine.Vector3(x, y, -10);

            elapsed += 1; // We are not using Time.deltaTime in this project, so this counts up every frame.

            yield return null; // Wait until the next frame
        }

        // Return the camera to its original local position after while loop is done.
        transform.localPosition = originalLocalPosition;
    }
}
