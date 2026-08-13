using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class SubMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject DefaultSelected;
    [SerializeField] private GameObject currentlySelected;
    [SerializeField] private Button backButton;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollDivide;
    private TMP_Dropdown dropdown;
    private Toggle subDropdown;
    private enum ButtonType
    {
        Button,
        Toggle,
        Dropdown,
        SubDropdown,
        Slider,
        SubSlider
    }
    [SerializeField] private ButtonType buttonType;
    private void Start()
    {
        currentlySelected = DefaultSelected.GetComponent<Toggle>().FindSelectableOnDown().GetComponent<Toggle>().FindSelectableOnUp().gameObject;
        Select();
    }
    void Select()
    {
        if(currentlySelected.GetComponent<Toggle>() != null)
        {
            subDropdown = currentlySelected.GetComponent<Toggle>();
            dropdown = currentlySelected.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<TMP_Dropdown>();
            buttonType = ButtonType.SubDropdown;
            subDropdown.Select();
        }
        if(scrollRect != null)
        {
            SnapTo(currentlySelected.gameObject.GetComponent<RectTransform>());
        }
        
    }
    
    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
        Vector2 childLocalPosition = target.anchoredPosition;
        if(target.transform.parent.gameObject.name == "PlayerColor")
        {
            childLocalPosition = new Vector2(childLocalPosition.x,childLocalPosition.y-200);
        }
        scrollRect.content.localPosition = new Vector2(scrollRect.content.localPosition.x, 0 + (viewportLocalPosition.y + (childLocalPosition.y / -scrollDivide)+135));
    }
    void Update()
    {
        switch (buttonType)
        {
            case (ButtonType.SubDropdown):
                SubDropdownUpdate();
                break;
            
        }
    }
    void SubDropdownUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(subDropdown.FindSelectableOnDown() != null)
            {
                currentlySelected = subDropdown.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(subDropdown.FindSelectableOnUp() != null)
            {
                currentlySelected = subDropdown.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
        }
        if (UserInput.Instance.SubmitInput)
        {
            buttonType = ButtonType.SubDropdown;
            subDropdown.isOn = !subDropdown.isOn;
            dropdown.Hide();
        }
    }
}