using UnityEngine;

public class CameraAreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Camera"))
        {
            Debug.Log("Entered Transition Area");
        }
    }
}