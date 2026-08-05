using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class MenuNavigationController : MonoBehaviour
{
    [SerializeField] private GameObject DefaultSelected;
    [SerializeField] private GameObject currentlySelected;
    [SerializeField] private Button backButton;
    [SerializeField] private Button NextMenuButton;
    [SerializeField] private Button LastMenuButton;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollDivide;
    private Button button;
    private Toggle toggle;
    private Slider slider;
    private TMP_Dropdown dropdown;
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
    private Animator anim;
    private void Start()
    {
        currentlySelected = DefaultSelected;
        Select();
    }
    private void OnEnable()
    {
        Select();
    }
    void Select()
    {
        if(currentlySelected.GetComponent<Button>() != null)
        {
            button = currentlySelected.GetComponent<Button>();
            buttonType = ButtonType.Button;
            button.Select();
        }
        if(currentlySelected.GetComponent<Toggle>() != null)
        {
            toggle = currentlySelected.GetComponent<Toggle>();
            buttonType = ButtonType.Toggle;
            toggle.Select();
        }
        if(currentlySelected.GetComponent<Slider>() != null)
        {
            slider = currentlySelected.GetComponent<Slider>();
            anim = currentlySelected.GetComponent<Animator>();
            buttonType = ButtonType.Slider;
            slider.Select();
        }
        if(currentlySelected.GetComponent<TMP_Dropdown>() != null)
        {
            dropdown = currentlySelected.GetComponent<TMP_Dropdown>();
            buttonType = ButtonType.Dropdown;
            dropdown.Select();
        }
        if(scrollRect != null)
        {
            SnapTo(currentlySelected.transform.parent.gameObject.GetComponent<RectTransform>());
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
        scrollRect.content.localPosition = new Vector2(0, 0 + (viewportLocalPosition.y + (childLocalPosition.y / -scrollDivide)-2f));
    }
    void LateUpdate()
    {
        switch (buttonType)
        {
            case (ButtonType.Button):
                ButtonUpdate();
                break;
            case (ButtonType.Toggle):
                ToggleUpdate();
                break;
            case (ButtonType.Slider):
                SliderUpdate();
                break;
            case (ButtonType.SubSlider):
                SubSliderUpdate();
                break;
            case (ButtonType.Dropdown):
                DropdownUpdate();
                break;
            
        }
        if (UserInput.Instance.RightMenuInput && NextMenuButton != null)
        {
            NextMenuButton.onClick.Invoke();
        }
        if (UserInput.Instance.LeftMenuInput && LastMenuButton != null)
        {
            LastMenuButton.onClick.Invoke();
        }
    }
    void ButtonUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = button.FindSelectableOnDown().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = button.FindSelectableOnUp().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = button.FindSelectableOnLeft().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = button.FindSelectableOnRight().gameObject;
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            button = backButton;
            buttonType = ButtonType.Button;
            button.onClick.Invoke();
        }
        if (UserInput.Instance.SubmitInput)
        {
            Select();
            button.onClick.Invoke();
        }
    }
    void ToggleUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = toggle.FindSelectableOnDown().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = toggle.FindSelectableOnUp().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = toggle.FindSelectableOnLeft().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = toggle.FindSelectableOnRight().gameObject;
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            button = backButton;
            buttonType = ButtonType.Button;
            button.onClick.Invoke();
        }
        if (UserInput.Instance.SubmitInput)
        {
            Select();
            toggle.isOn = !toggle.isOn;
        }
    }
    void DropdownUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = dropdown.FindSelectableOnDown().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = dropdown.FindSelectableOnUp().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = dropdown.FindSelectableOnLeft().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = dropdown.FindSelectableOnRight().gameObject;
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            button = backButton;
            buttonType = ButtonType.Button;
            button.onClick.Invoke();
        }
        if (UserInput.Instance.SubmitInput)
        {
            Select();
        }
    }
    void SliderUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnDown().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnUp().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnLeft().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnRight().gameObject;
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            button = backButton;
            buttonType = ButtonType.Button;
            button.onClick.Invoke();
        }
        if (UserInput.Instance.SubmitInput)
        {
            buttonType = ButtonType.SubSlider;
        }
    }
    void SubSliderUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnDown().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            currentlySelected = slider.FindSelectableOnUp().gameObject;
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f)
        {
            if (!slider.wholeNumbers)
            {
                slider.value -= 0.01f;
            }
            else if(UserInput.Instance.NavigateDown)
            {
                slider.value -= 1;
            }
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f)
        {
            if (!slider.wholeNumbers)
            {
                slider.value += 0.01f;
            }
            else if(UserInput.Instance.NavigateDown)
            {
                slider.value += 1;
            }
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            buttonType = ButtonType.Slider;
            Select();
        }
        if (UserInput.Instance.SubmitInput)
        {
            buttonType = ButtonType.Slider;
            Select();
        }
    }
}