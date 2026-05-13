using System;
using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float swayX = 0;
    [SerializeField] private bool swayXDir = false;
    [SerializeField] private float swayY = 0;
    [SerializeField] private bool swayYDir = false;
    public float parallaxValue;
    public float offsetX;
    public float offsetY;
    void Update()
    {
        if(Input.mousePosition.x != 0 && Input.mousePosition.y != 0)
        {
            rectTransform.position = new Vector2((Input.mousePosition.x * parallaxValue)+(Screen.width/2)+offsetX,(Input.mousePosition.y * parallaxValue)+(Screen.height/2)+offsetY);
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
    }
}
