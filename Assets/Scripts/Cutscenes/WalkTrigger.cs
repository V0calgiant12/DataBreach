using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        CutsceneManager.Instance.walking = false;
    }
}