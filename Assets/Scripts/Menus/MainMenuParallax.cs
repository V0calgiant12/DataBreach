using System;
using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    public float parallaxValue;
    public float offsetX;
    public float offsetY;
    void Update()
    {
        if(Input.mousePosition.x != 0 && Input.mousePosition.y != 0)
        {
            rectTransform.position = new Vector2((Input.mousePosition.x * parallaxValue)+(Screen.width/2)+offsetX,(Input.mousePosition.y * parallaxValue)+(Screen.height/2)+offsetY);
        }
    }
}
