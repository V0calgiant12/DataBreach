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
    [SerializeField] private float swayX = 0;
    static public float randomSwayX;
    [SerializeField] private float swayY = 0;
    static public float randomSwayY;
    public float swaySpeed;
    [SerializeField] private float timer;
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
            //Debug.Log(mainCamera.aspect);
            rectTransform.position = new Vector2((Input.mousePosition.x * parallaxValue/mainCamera.aspect)+(Screen.width/2)+offsetX,(Input.mousePosition.y * parallaxValue)+(Screen.height/2)+offsetY);
        }
        timer += Time.deltaTime;
        swayX = Mathf.Sin(timer * swaySpeed + randomSwayX) * 5;
        swayY = MathF.Cos(timer * swaySpeed + randomSwayY) * 5;
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
