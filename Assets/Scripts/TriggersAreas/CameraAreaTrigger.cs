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
            cameraLocation.limitX = limitX == 0 ? limitX = float.NaN : limitX;
            cameraLocation.limitY = limitY == 0 ? limitY = float.NaN : limitY;
            cameraLocation.flippable = flippable;
            cameraLocation.side = side;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Exited Camera Area");
            CameraLocationUpdater cameraLocation = other.gameObject.GetComponent<CameraLocationUpdater>();
            cameraLocation.limitX = limitX = float.NaN;
            cameraLocation.limitY = limitY = float.NaN;
        }
    }
}