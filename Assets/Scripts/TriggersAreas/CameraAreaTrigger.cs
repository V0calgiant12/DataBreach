using UnityEngine;

public class CameraAreaTrigger : MonoBehaviour
{
    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    [SerializeField] private bool flippable;
    [SerializeField] private CameraLocationUpdater.Side side;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Entered Camera Area");
            CameraLocationUpdater cameraLocation = other.gameObject.GetComponent<CameraLocationUpdater>();
            cameraLocation.limitX = limitX;
            cameraLocation.flippable = flippable;
            cameraLocation.side = side;
        }
    }
}