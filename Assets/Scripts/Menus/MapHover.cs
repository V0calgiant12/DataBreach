using UnityEngine;
using UnityEngine.EventSystems;

public class MapHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject HoverVisual;
    void Start()
    {
        HoverVisual.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Time.timeScale == 1)
        {
            HoverVisual.SetActive(true);
        }
    }
    public void OnPointerOver(PointerEventData eventData)
    {
        if(Time.timeScale != 1)
        {
            HoverVisual.SetActive(false);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        HoverVisual.SetActive(false);
    }
}