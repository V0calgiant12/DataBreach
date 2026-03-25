
using UnityEngine;

public class CameraUpdater : MonoBehaviour 
{
    [SerializeField] private GameObject cameraLoc;
    private Vector3 Velocity = Vector3.zero;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraLoc.transform.position, ref Velocity, 0.2f);
        // Stops camera from "Sliding"
        if (!Input.anyKey && Velocity.x < 1 && Velocity.x > -1)
        {
            Velocity = new Vector2(0, Velocity.y);
        }
        if (!Input.anyKey && Velocity.y < 1 && Velocity.y > -1)
        {
            Velocity = new Vector2(Velocity.x, 0);
        }
    }
}