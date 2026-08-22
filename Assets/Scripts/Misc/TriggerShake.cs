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
    void Update()
    {
        if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.B))
        {
            BurstShake(15,1f,true,0);
        }
        if (Input.GetKey(KeyCode.F3) && Input.GetKeyDown(KeyCode.S))
        {
            Shake(60,15);
        }
    }
    public void Shake(int duration,float magnitude)
    {
        StartCoroutine(CameraShaker.Instance.Shake(duration, magnitude));
    }
    public void BurstShake(float magnitude,float lengthMult, bool playSound,float overrideVolume)
    {
        StartCoroutine(CameraShaker.Instance.BurstShake(magnitude,lengthMult,playSound,overrideVolume));
    }
}
