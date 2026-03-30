using UnityEngine;
/// <summary>
/// Used for when a script is a monobehavior that can't call the coroutine itself.
/// </summary>
public class TriggerShake : MonoBehaviour 
{
    public static TriggerShake Instance;
    void Start() 
    {
        Instance = this;
    }
    public void Shake(int duration,float magnitude)
    {
        StartCoroutine(CameraShaker.Instance.Shake(duration, magnitude));
    }
    public void BurstShake(float magnitude)
    {
        StartCoroutine(CameraShaker.Instance.BurstShake(magnitude));
    }
}
