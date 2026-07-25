using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    [Tooltip("Time (in frames) the object will wait until deleting itself.")]
    public int time;
    [Tooltip("Whether or not it will take into account the game being paused.")]
    public bool pausable;
    private int timeLeft = 0;
    void Start()
    {
        timeLeft = time;
    }
    void Update()
    {
        timeLeft -= Time.timeScale == 1 ? 1 : pausable ? 0 : 1;
    }
}