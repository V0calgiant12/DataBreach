using NUnit.Framework.Internal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayInputAsText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private GameObject icon;
    [SerializeField] private SpriteRenderer sr;
    private enum InputType
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Jump,
        Sprint,
        Attack,
        Interact,
        // Here down is specific icons for Keyboard & Mouse controlls.
        LeftStick,
        RightStick,
        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow,
        MouseLeft,
        MouseRight,
        MouseMiddle,
        MouseForward,
        MouseBack,
        Space
    }
    [SerializeField] private InputType inputDisplayed;
    void Start()
    {
        InvokeRepeating("OffsetUpdate",0,0.5f);
    }
    private void OffsetUpdate()
    {
        if(UserInput.Instance.currentController == UserInput.ControllerSchemes.Controller)
        {
            icon.SetActive(true);
            text.text = " ";
            GetIconOfInput(inputDisplayed);
        }
        else
        {
            icon.SetActive(false);
            GetOutputOfKey(inputDisplayed);
        }
    }
    private void GetOutputOfKey(InputType input)
    {
        KeyCode keyCodeOut = KeyCode.None;
        switch (input)
        {
            case(InputType.MoveUp):
                keyCodeOut = SettingsData.Instance._InputUp;
                break;
            case(InputType.MoveDown):
                keyCodeOut = SettingsData.Instance._InputDown;
                break;
            case(InputType.MoveLeft):
                keyCodeOut = SettingsData.Instance._InputLeft;
                break;
            case(InputType.MoveRight):
                keyCodeOut = SettingsData.Instance._InputRight;
                break;
            case(InputType.Jump):
                keyCodeOut = SettingsData.Instance._InputJump;
                break;
            case(InputType.Sprint):
                keyCodeOut = SettingsData.Instance._InputSprint;
                break;
            case(InputType.Attack):
                keyCodeOut = SettingsData.Instance._InputAttack;
                break;
            case(InputType.Interact):
                keyCodeOut = SettingsData.Instance._InputInteract;
                break;
        }
        switch(keyCodeOut)
        {
            case(KeyCode.UpArrow):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.UpArrow);
                return;
            case(KeyCode.DownArrow):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.DownArrow);
                return;
            case(KeyCode.LeftArrow):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.LeftArrow);
                return;
            case(KeyCode.RightArrow):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.RightArrow);
                return;
            case(KeyCode.Mouse0):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.MouseLeft);
                return;
            case(KeyCode.Mouse1):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.MouseRight);
                return;
            case(KeyCode.Mouse2):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.MouseMiddle);
                return;
            case(KeyCode.Mouse3):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.MouseBack);
                return;
            case(KeyCode.Mouse4):
                icon.SetActive(true);
                text.text = " ";
                GetIconOfInput(InputType.MouseForward);
                return;
            case(KeyCode.Space):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.Space);
                return;
        }
        text.text = "" + keyCodeOut;
    }
    private void GetIconOfInput(InputType input)
    {
        string path = "";
        switch (input)
        {
            case(InputType.MoveUp):
                path = "UI/ControllerIcons/LeftStickUp";
                break;
            case(InputType.MoveDown):
                path = "UI/ControllerIcons/LeftStickDown";
                break;
            case(InputType.MoveLeft):
                path = "UI/ControllerIcons/LeftStickLeft";
                break;
            case(InputType.MoveRight):
                path = "UI/ControllerIcons/LeftStickRight";
                break;
            case(InputType.Jump):
                path = UserInput.Instance.controllerType == UserInput.ControllerTypes.PS ? "UI/ControllerIcons/SquareButton" : UserInput.Instance.controllerType == UserInput.ControllerTypes.Switch ? "UI/ControllerIcons/YButton":"UI/ControllerIcons/XButton";
                break;
            case(InputType.Sprint):
                path = UserInput.Instance.controllerType == UserInput.ControllerTypes.PS ? "UI/ControllerIcons/CrossButton" : UserInput.Instance.controllerType == UserInput.ControllerTypes.Switch ? "UI/ControllerIcons/BButton":"UI/ControllerIcons/AButton";
                break;
            case(InputType.Attack):
                path = UserInput.Instance.controllerType == UserInput.ControllerTypes.PS ? "UI/ControllerIcons/CircleButton" : UserInput.Instance.controllerType == UserInput.ControllerTypes.Switch ? "UI/ControllerIcons/AButton":"UI/ControllerIcons/BButton";
                break;
            case(InputType.Interact):
                path = UserInput.Instance.controllerType == UserInput.ControllerTypes.PS ? "UI/ControllerIcons/TriangleButton" : UserInput.Instance.controllerType == UserInput.ControllerTypes.Switch ? "UI/ControllerIcons/XButton":"UI/ControllerIcons/YButton";
                break;
            case(InputType.LeftStick):
                path = "UI/ControllerIcons/LeftStick";
                break;
            case(InputType.RightStick):
                path = "UI/ControllerIcons/RightStick";
                break;
            case(InputType.UpArrow):
                path = "UI/ControllerIcons/UpArrow";
                break;
            case(InputType.DownArrow):
                path = "UI/ControllerIcons/DownArrow";
                break;
            case(InputType.LeftArrow):
                path = "UI/ControllerIcons/LeftArrow";
                break;
            case(InputType.RightArrow):
                path = "UI/ControllerIcons/RightArrow";
                break;
            case(InputType.MouseLeft):
                path = "UI/ControllerIcons/MouseLeft";
                break;
            case(InputType.MouseRight):
                path = "UI/ControllerIcons/MouseRight";
                break;
            case(InputType.MouseMiddle):
                path = "UI/ControllerIcons/MouseMiddle";
                break;
            case(InputType.MouseForward):
                path = "UI/ControllerIcons/MouseForward";
                break;
            case(InputType.MouseBack):
                path = "UI/ControllerIcons/MouseBack";
                break;
            case(InputType.Space):
                path = "UI/ControllerIcons/Space";
                break;
        }
        sr.sprite = Resources.Load<Sprite>(path);
    }
}