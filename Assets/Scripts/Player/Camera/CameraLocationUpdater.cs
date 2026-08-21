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
        Bottom,
        None
    }
    public Side HorizontalSide = Side.None;
    public Side VerticalSide = Side.None;
    [SerializeField] private float cameraWidth;
    [SerializeField] private float cameraHeight;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void LateUpdate()
    {
        cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        cameraHeight = mainCamera.orthographicSize;
        //Debug.Log(transform.position.x + cameraWidth);
        transform.position = new UnityEngine.Vector3(player.transform.position.x, player.transform.position.y, -10);
        if(HorizontalSide != Side.None && limitX != float.NaN)
        {
            if(HorizontalSide == Side.Right && limitX < transform.position.x + cameraWidth)
            {
                transform.position = new UnityEngine.Vector3(limitX - cameraWidth, transform.position.y, -10);
                if(player.transform.position.x > limitX && flippable)
                {
                    VerticalSide = Side.Left;
                }
            }
            else if(HorizontalSide == Side.Left && limitX > transform.position.x - cameraWidth)
            {
                transform.position = new UnityEngine.Vector3(limitX + cameraWidth, transform.position.y, -10);
                if(player.transform.position.x < limitX && flippable)
                {
                    VerticalSide = Side.Right;
                }
            }
            else
            {
                transform.position = new UnityEngine.Vector3(transform.position.x + rb.linearVelocityX/4, transform.position.y, -10);
            }
        }
        if(VerticalSide != Side.None && limitY != float.NaN)
        {
            if(VerticalSide == Side.Top && limitY < transform.position.y + cameraHeight)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x, limitY - cameraHeight, -10);
                if(player.transform.position.y > limitY && flippable)
                {
                    VerticalSide = Side.Bottom;
                }
            }
            else if(VerticalSide == Side.Bottom && limitY > transform.position.y - cameraHeight)
            {
                transform.position = new UnityEngine.Vector3(transform.position.x, limitY + cameraHeight, -10);
                if(player.transform.position.y < limitY && flippable)
                {
                    VerticalSide = Side.Top;
                }
            }
            else
            {
                transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y + rb.linearVelocityY/4, -10);
            }
        }
    }
}