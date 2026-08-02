using UnityEngine;

public class CameraAreaTrigger : MonoBehaviour
{
    [SerializeField] private float limitX;
    [SerializeField] private float limitY;
    [SerializeField] private bool flippable;
    [SerializeField] private bool resetOnExit = true;
    [SerializeField] private bool detectPlayer = false;
    [SerializeField] private CameraLocationUpdater.Side HorizontalSide;
    [SerializeField] private CameraLocationUpdater.Side VerticalSide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("RealCamera") && !detectPlayer)
        {
            Enter(other);
        }
        else if(other.gameObject.CompareTag("Player") && detectPlayer)
        {
            Enter(other);
        }
    }
    private void Enter(Collider2D other)
    {
        Debug.Log("Entered Camera Area");
        CameraLocationUpdater cameraLocation = GameObject.Find("CameraLocation").GetComponent<CameraLocationUpdater>();
        cameraLocation.limitX = limitX == 0 ? limitX = float.NaN : limitX;
        cameraLocation.limitY = limitY == 0 ? limitY = float.NaN : limitY;
        cameraLocation.flippable = flippable;
        cameraLocation.HorizontalSide = HorizontalSide;
        cameraLocation.VerticalSide = VerticalSide;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (resetOnExit)
        {
            if (other.gameObject.CompareTag("RealCamera") && !detectPlayer)
            {
                Exit(other);
            }
            else if (other.gameObject.CompareTag("Player") && detectPlayer)
            {
                Exit(other);
            }
        }
    }
    private void Exit(Collider2D other)
    {
        Debug.Log("Exited Camera Area");
        CameraLocationUpdater cameraLocation = GameObject.Find("CameraLocation").GetComponent<CameraLocationUpdater>();
        cameraLocation.limitX = float.NaN;
        cameraLocation.limitY = float.NaN;
        cameraLocation.HorizontalSide = CameraLocationUpdater.Side.None;
        cameraLocation.VerticalSide = CameraLocationUpdater.Side.None;
    }
}