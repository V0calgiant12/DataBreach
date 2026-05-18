using System;
using Unity.VisualScripting;
using UnityEngine;

public class MapParallax : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    private Camera mainCamera;
    [SerializeField] private Vector4 edgePos;
    [SerializeField] private RectTransform TopRight;
    [SerializeField] private RectTransform BottomLeft;
    private float swayX = 0;
    private bool swayXDir = false;
    private float swayY = 0;
    private bool swayYDir = false;
    public float parallaxValue;
    public float offsetX;
    public float offsetY;
    public Vector2 MinPos;
    public Vector2 MaxPos;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        if(Input.mousePosition.x != 0 && Input.mousePosition.y != 0)
        {
            Debug.Log(mainCamera.aspect);
            rectTransform.position = new Vector2((Input.mousePosition.x * parallaxValue/mainCamera.aspect)+(Screen.width/2)+offsetX,(Input.mousePosition.y * parallaxValue)+(Screen.height/2)+offsetY);
        }
        swayX += (swayXDir == true) ? 0.01f : -0.01f;
        swayY += (swayYDir == true) ? 0.01f : -0.01f;
        if(swayX >= 2 || swayX <= -2)
        {
            swayXDir = (swayX < 0) ? true : false;
        }
        if(swayY >= 1 || swayY <= -1)
        {
            swayYDir = (swayY < 0) ? true : false;
        }
        rectTransform.position = new Vector2(rectTransform.position.x + swayX, rectTransform.position.y + swayY);

        if(TopRight.position.x < MaxPos.x && MaxPos.x != 0)
        {
            rectTransform.position = new Vector2(MaxPos.x, rectTransform.position.y);
        }
        if(BottomLeft.position.x > MinPos.x && MinPos.x != 0)
        {
            rectTransform.position = new Vector2(MinPos.x, rectTransform.position.y);
        }
        if(TopRight.position.y < MaxPos.y && MaxPos.y != 0)
        {
            rectTransform.position = new Vector2(rectTransform.position.x, MaxPos.y - rectTransform.rect.height/2);
        }
        if(BottomLeft.position.y > MinPos.y && MinPos.y != 0)
        {
            rectTransform.position = new Vector2(rectTransform.position.x, MinPos.y + rectTransform.rect.height/2);
        }
    }
}
