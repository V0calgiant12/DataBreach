using UnityEngine;

public class CutsceneProgressionTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IntroCutsceneManager.Instance.falling = false;
    }
}