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
        Space,
        LeftAlt,
        RightAlt,
        LeftShift,
        RightShift,
        LeftCtrl,
        RightCtrl,
        CapsLock,
        Tab,
        Enter,
        Backspace,
        NumLock
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
            case(KeyCode.Backslash):
                text.text = "\\";
                return;
            case(KeyCode.Equals):
                text.text = "=";
                return;
            case(KeyCode.Minus):
                text.text = "-";
                return;
            case(KeyCode.KeypadPlus):
                text.text = "+";
                return;
            case(KeyCode.KeypadMinus):
                text.text = "-";
                return;
            case(KeyCode.KeypadMultiply):
                text.text = "*";
                return;
            case(KeyCode.KeypadDivide):
                text.text = "/";
                return;
            case(KeyCode.Alpha0):
                text.text = "0";
                return;
            case(KeyCode.Alpha9):
                text.text = "9";
                return;
            case(KeyCode.Alpha8):
                text.text = "8";
                return;
            case(KeyCode.Alpha7):
                text.text = "7";
                return;
            case(KeyCode.Alpha6):
                text.text = "6";
                return;
            case(KeyCode.Alpha5):
                text.text = "5";
                return;
            case(KeyCode.Alpha4):
                text.text = "4";
                return;
            case(KeyCode.Alpha3):
                text.text = "3";
                return;
            case(KeyCode.Alpha2):
                text.text = "2";
                return;
            case(KeyCode.Alpha1):
                text.text = "1";
                return;
            case(KeyCode.Keypad0):
                text.text = "0";
                return;
            case(KeyCode.Keypad9):
                text.text = "9";
                return;
            case(KeyCode.Keypad8):
                text.text = "8";
                return;
            case(KeyCode.Keypad7):
                text.text = "7";
                return;
            case(KeyCode.Keypad6):
                text.text = "6";
                return;
            case(KeyCode.Keypad5):
                text.text = "5";
                return;
            case(KeyCode.Keypad4):
                text.text = "4";
                return;
            case(KeyCode.Keypad3):
                text.text = "3";
                return;
            case(KeyCode.Keypad2):
                text.text = "2";
                return;
            case(KeyCode.Keypad1):
                text.text = "1";
                return;
            case(KeyCode.BackQuote):
                text.text = "`";
                return;
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
            case(KeyCode.LeftAlt):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.LeftAlt);
                return;
            case(KeyCode.RightAlt):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.RightAlt);
                return;
            case(KeyCode.LeftShift):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.LeftShift);
                return;
            case(KeyCode.RightShift):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.RightShift);
                return;
            case(KeyCode.LeftControl):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.LeftCtrl);
                return;
            case(KeyCode.RightControl):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.RightCtrl);
                return;
            case(KeyCode.CapsLock):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.CapsLock);
                return;
            case(KeyCode.Return):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.Enter);
                return;
            case(KeyCode.KeypadEnter):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.Enter);
                return;
            case(KeyCode.Tab):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.Tab);
                return;
            case(KeyCode.Backspace):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.Backspace);
                return;
            case(KeyCode.Numlock):
                icon.SetActive(true);
                text.text = "  ";
                GetIconOfInput(InputType.NumLock);
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
            case(InputType.LeftAlt):
                path = "UI/ControllerIcons/LAlt";
                break;
            case(InputType.RightAlt):
                path = "UI/ControllerIcons/RAlt";
                break;
            case(InputType.LeftShift):
                path = "UI/ControllerIcons/LShift";
                break;
            case(InputType.RightShift):
                path = "UI/ControllerIcons/RShift";
                break;
            case(InputType.LeftCtrl):
                path = "UI/ControllerIcons/LCtrl";
                break;
            case(InputType.RightCtrl):
                path = "UI/ControllerIcons/RCtrl";
                break;
            case(InputType.CapsLock):
                path = "UI/ControllerIcons/CapsLock";
                break;
            case(InputType.Tab):
                path = "UI/ControllerIcons/Tab";
                break;
            case(InputType.Enter):
                path = "UI/ControllerIcons/Enter";
                break;
            case(InputType.Backspace):
                path = "UI/ControllerIcons/Backspace";
                break;
            case(InputType.NumLock):
                path = "UI/ControllerIcons/NumLock";
                break;
            
        }
        sr.sprite = Resources.Load<Sprite>(path);
    }
}