using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    [Tooltip("Time (in frames) the object will wait until deleting itself.")]
    public float time;
    [Tooltip("Whether or not it will take into account the game being paused.")]
    public bool pausable;
    [Tooltip("Should this be counted by frames or delta time? Overrides pausable option, setting it to be true.")]
    public bool deltaTime;
    private float timeLeft = 0;
    void Start()
    {
        timeLeft = time;
    }
    void Update()
    {
        if (deltaTime)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft -= Time.timeScale == 1 ? 1 : pausable ? 0 : 1;
        }
        if(timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }
}