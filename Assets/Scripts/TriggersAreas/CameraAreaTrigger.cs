using UnityEngine;

public class CameraAreaTrigger : MonoBehaviour
{
    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    [SerializeField] private float flippable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Entered Transition Area");
            CameraLocationUpdater cameraLocation = other.gameObject.GetComponent<CameraLocationUpdater>();
            cameraLocation.limitX = limitX;
        }
    }
}