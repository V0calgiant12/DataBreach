using UnityEngine;

public class CutsceneProgressionTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GeneralCutsceneManager.Instance.falling = false;
    }
}