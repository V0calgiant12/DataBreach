using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;
using TMPro;

#pragma warning disable 864121
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
    private Toggle subDropdown;
    private Selectable selectable;
    private enum ButtonType
    {
        Button,
        Toggle,
        Dropdown,
        SubDropdown,
        Slider,
        SubSlider,
        Selectable
    }
    [SerializeField] private ButtonType buttonType;
    private Animator anim;
    private void Awake()
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
        else if(currentlySelected.GetComponent<Toggle>() != null)
        {
            toggle = currentlySelected.GetComponent<Toggle>();
            buttonType = ButtonType.Toggle;
            toggle.Select();
        }
        else if(currentlySelected.GetComponent<Slider>() != null)
        {
            slider = currentlySelected.GetComponent<Slider>();
            anim = currentlySelected.GetComponent<Animator>();
            buttonType = ButtonType.Slider;
            slider.Select();
        }
        else if(currentlySelected.GetComponent<TMP_Dropdown>() != null)
        {
            dropdown = currentlySelected.GetComponent<TMP_Dropdown>();
            buttonType = ButtonType.Dropdown;
            dropdown.Select();
        }
        else if(currentlySelected.GetComponent<Selectable>() != null)
        {
            selectable = currentlySelected.GetComponent<Selectable>();
            buttonType = ButtonType.Selectable;
            selectable.Select();
        }
        else
        {
            Debug.LogError("ERROR: No selectable (or adjacent) component found.",currentlySelected);
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
        scrollRect.content.localPosition = new Vector2(scrollRect.content.localPosition.x, 0 + (viewportLocalPosition.y + (childLocalPosition.y / -scrollDivide)-2f));
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
            case (ButtonType.SubDropdown):
                SubDropdownUpdate();
                break;
            case (ButtonType.Selectable):
                SelectableUpdate();
                break;
            
        }
        if (UserInput.Instance.RightMenuInput && NextMenuButton != null && buttonType != ButtonType.SubDropdown)
        {
            NextMenuButton.onClick.Invoke();
        }
        if (UserInput.Instance.LeftMenuInput && LastMenuButton != null && buttonType != ButtonType.SubDropdown)
        {
            LastMenuButton.onClick.Invoke();
        }
    }
    void ButtonUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(button.FindSelectableOnDown() != null)
            {
                currentlySelected = button.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(button.FindSelectableOnUp() != null)
            {
                currentlySelected = button.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(button.FindSelectableOnLeft() != null)
            {
                currentlySelected = button.FindSelectableOnLeft().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(button.FindSelectableOnRight() != null)
            {
                currentlySelected = button.FindSelectableOnRight().gameObject;
            }
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
            if(toggle.FindSelectableOnDown() != null)
            {
                currentlySelected = toggle.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(toggle.FindSelectableOnUp() != null)
            {
                currentlySelected = toggle.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(toggle.FindSelectableOnLeft() != null)
            {
                currentlySelected = toggle.FindSelectableOnLeft().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(toggle.FindSelectableOnRight() != null)
            {
                currentlySelected = toggle.FindSelectableOnRight().gameObject;
            }
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
            if(dropdown.FindSelectableOnDown() != null)
            {
                currentlySelected = dropdown.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(dropdown.FindSelectableOnUp() != null)
            {
                currentlySelected = dropdown.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(dropdown.FindSelectableOnLeft() != null)
            {
                currentlySelected = dropdown.FindSelectableOnLeft().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(dropdown.FindSelectableOnRight() != null)
            {
                currentlySelected = dropdown.FindSelectableOnRight().gameObject;
            }
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
            buttonType = ButtonType.SubDropdown;
            dropdown.Show();
        }
    }
    void SubDropdownUpdate()
    {
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            dropdown.Hide();
            buttonType = ButtonType.Dropdown;
            Select();
        }
        if (UserInput.Instance.SubmitInput)
        {
            dropdown.Hide();
            buttonType = ButtonType.Dropdown;
            Select();
        }
    }
    void SliderUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(slider.FindSelectableOnDown() != null)
            {
                currentlySelected = slider.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(slider.FindSelectableOnUp() != null)
            {
                currentlySelected = slider.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(slider.FindSelectableOnLeft() != null)
            {
                currentlySelected = slider.FindSelectableOnLeft().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(slider.FindSelectableOnRight() != null)
            {
                currentlySelected = slider.FindSelectableOnRight().gameObject;
            }
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
            if(slider.FindSelectableOnDown() != null)
            {
                currentlySelected = slider.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(slider.FindSelectableOnUp() != null)
            {
                currentlySelected = slider.FindSelectableOnUp().gameObject;
            }
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
    void SelectableUpdate()
    {
        
        if (UserInput.Instance.NavigateInput.y < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(selectable.FindSelectableOnDown() != null)
            {
                currentlySelected = selectable.FindSelectableOnDown().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.y > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(selectable.FindSelectableOnUp() != null)
            {
                currentlySelected = selectable.FindSelectableOnUp().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x < -0.5f && UserInput.Instance.NavigateDown)
        {
            if(selectable.FindSelectableOnLeft() != null)
            {
                currentlySelected = selectable.FindSelectableOnLeft().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.NavigateInput.x > 0.5f && UserInput.Instance.NavigateDown)
        {
            if(selectable.FindSelectableOnRight() != null)
            {
                currentlySelected = selectable.FindSelectableOnRight().gameObject;
            }
            Select();
        }
        if (UserInput.Instance.CancelInput && backButton != null)
        {
            button = backButton;
            buttonType = ButtonType.Button;
            button.onClick.Invoke();
        }
    }
}