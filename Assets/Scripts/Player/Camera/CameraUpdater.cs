
using UnityEngine;

public class CameraUpdater : MonoBehaviour 
{
    [SerializeField] private GameObject cameraLoc;
    private Vector3 Velocity = Vector3.zero;
    void Start()
    {
	    QualitySettings.vSyncCount = 1;
	    Application.targetFrameRate = 60;
    }
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraLoc.transform.position, ref Velocity, 0.2f);
    }
}