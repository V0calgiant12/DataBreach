using System;
using Unity.Mathematics;
using UnityEngine;

public class CameraLocationUpdater : MonoBehaviour 
{
    [Header("Camera Location Updater References:")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    private Camera mainCamera;
    public float limitX = float.NaN;
    public float limitY = float.NaN;
    public bool flippable = false;
    public enum Side
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public Side side;
    [SerializeField] private float cameraWidth;
    [SerializeField] private float cameraHeight;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        cameraHeight = mainCamera.orthographicSize;
        //Debug.Log(transform.position.x + cameraWidth);
        transform.position = new UnityEngine.Vector3(player.transform.position.x + rb.linearVelocityX/4, player.transform.position.y + rb.linearVelocityY/4, -10);
        Limits();
    }
    void Limits()
    {
        if(limitX != float.NaN)
        {
            if(side == Side.Right && limitX < transform.position.x + cameraWidth)
            {
                transform.position = new UnityEngine.Vector3(limitX - cameraWidth, transform.position.y, -10);
            }
            else if(side == Side.Left && limitX > transform.position.x - cameraWidth)
            {
                transform.position = new UnityEngine.Vector3(limitX + cameraWidth, transform.position.y, -10);
            }
        }
        if(limitY != float.NaN)
        {
            if(side == Side.Top && limitY < transform.position.y + cameraHeight)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x, limitY - cameraHeight, -10);
            }
            else if(side == Side.Bottom && limitY > transform.position.y - cameraHeight)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x, limitY + cameraHeight, -10);
            }
        }
    }
}