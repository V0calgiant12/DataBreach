using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ParticleTimer : MonoBehaviour
{
    private int timer;
    [Tooltip("The amount of time the object will exist for before destroying itself. (In frames)")]
    public int maxTime = 180;
    void Start()
    {
        timer = maxTime;
    }
    private void Update()
    {
        timer -= 1;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}