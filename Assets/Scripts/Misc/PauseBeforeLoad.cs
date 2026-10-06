using Unity.VisualScripting;
using UnityEngine;

public class PauseBeforeLoad : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public void Pause()
    {
        transition.updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
}