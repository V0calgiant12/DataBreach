using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IntroCutsceneManager.Instance.walking = false;
    }
}