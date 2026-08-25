using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class KeybindsController : MonoBehaviour
{
    public KeyCode _InputLeft = KeyCode.LeftArrow; // 0
    public KeyCode _InputRight = KeyCode.RightArrow; // 1
    public KeyCode _InputUp = KeyCode.UpArrow; // 2
    public KeyCode _InputDown = KeyCode.DownArrow; // 3
    public KeyCode _InputJump = KeyCode.Space; // 4
    public KeyCode _InputSprint = KeyCode.X; // 5
    public KeyCode _InputAttack = KeyCode.Z; // 6
    public KeyCode _InputParry = KeyCode.V; // 7
    public KeyCode _InputInteract = KeyCode.C; // 8
    public KeyCode _InputMenuLeft = KeyCode.LeftArrow; // 9
    public KeyCode _InputMenuRight = KeyCode.RightArrow; // 10
    public KeyCode _InputMenuUp = KeyCode.UpArrow; // 11
    public KeyCode _InputMenuDown = KeyCode.DownArrow; // 12
    public KeyCode _InputSubmit = KeyCode.C; // 13
    public KeyCode _InputCancel = KeyCode.X; // 14
    public KeyCode _InputForward = KeyCode.V; // 15
    public KeyCode _InputBack = KeyCode.Z; // 16
    public bool _UpToJump = false;
    private KeyCode currentKeyDown;
    private string objectName;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private MenuNavigationController navController;
    void OnEnable() // Refreshes settings on load.
    {
        StartCoroutine(RefreshSettings());
    }
    public void GetName(string name) // Gets object name. Im actually not entirely sure if this code still runs anywhere but it's staying just incase it does.
    {
        objectName = name;
        //Debug.Log(objectName);
    }
    public void ListenForKey(SettingsButtonData data) // Initial function called when a button is pressed for rebinding. Has button data that is unique to each button.
    {
        buttonText = data._TextMesh;
        button = data.GetComponent<Button>();
        button.interactable = false;
        navController.enableNavigation = false;
        StartCoroutine(StartListeningForKey(data._SettingID));
    }
    private IEnumerator StartListeningForKey(int inputNumber) // Listens for the next key to be pressed and acts accordingly when it does.
    {
        
        buttonText.text = "Press any key...";
        int elapsed = 0;
        while(elapsed < 5) // Wait 5 frames so that it doesn't INSTANTLY get set to a key you just pressed to select this.
        {
            elapsed += 1;
            yield return null;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);
        switch(inputNumber){ // This is a fancy if statement that only checks for the next item if the previous when was false.
            case  0:
                _InputLeft = currentKeyDown;
                break;
            case  1:
                _InputRight = currentKeyDown;
                break;
            case  2:
                _InputUp = currentKeyDown;
                break;
            case  3:
                _InputDown = currentKeyDown;
                break;
            case  4:
                _InputJump = currentKeyDown;
                break;
            case  5:
                _InputSprint = currentKeyDown;
                break;
            case  6:
                _InputAttack = currentKeyDown;
                break;
            case  7:
                _InputParry = currentKeyDown;
                break;
            case  8:
                _InputInteract = currentKeyDown;
                break;
            case  9:
                _InputMenuLeft = currentKeyDown;
                break;
            case  10:
                _InputMenuRight = currentKeyDown;
                break;
            case  11:
                _InputMenuUp = currentKeyDown;
                break;
            case  12:
                _InputMenuDown = currentKeyDown;
                break;
            case  13:
                _InputSubmit = currentKeyDown;
                break;
            case  14:
                _InputCancel = currentKeyDown;
                break;
            case  15:
                _InputForward = currentKeyDown;
                break;
            case  16:
                _InputBack = currentKeyDown;
                break;
        }
        buttonText.text = "" + currentKeyDown;
        //yield return new WaitUntil(()=> )); wait until mouse up
        button.interactable = true;
        elapsed = 0;
        while(elapsed != 2)
        {
            elapsed++;
            yield return null;
        }
        navController.enableNavigation = true;
        navController.Select(false);
    }
    public void ToggleSetting(SettingsToggleData data) // Handles when a setting is toggled
    {
        switch(data._ToggleID)
        {
            case(0): // Up to jump
                _UpToJump = data.toggle.isOn;
                break;
        }
    }
    public IEnumerator RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "ControlsMenu" to refresh their visuals, which is handled elsewhere.
    {
        int elapsed = 0;
        while (elapsed != 1)
        {
            elapsed++;
            yield return null;
        }
        _InputLeft = SettingsData.Instance._InputLeft;
        _InputRight = SettingsData.Instance._InputRight;
        _InputUp = SettingsData.Instance._InputUp;
        _InputDown = SettingsData.Instance._InputDown;
        _InputJump = SettingsData.Instance._InputJump;
        _InputSprint = SettingsData.Instance._InputSprint;
        _InputAttack = SettingsData.Instance._InputAttack;
        _InputParry = SettingsData.Instance._InputParry;
        _InputInteract = SettingsData.Instance._InputInteract;
        _InputMenuLeft = SettingsData.Instance._InputMenuLeft;
        _InputMenuRight = SettingsData.Instance._InputMenuRight;
        _InputMenuUp = SettingsData.Instance._InputMenuUp;
        _InputMenuDown = SettingsData.Instance._InputMenuDown;
        _InputSubmit = SettingsData.Instance._InputSubmit;
        _InputCancel = SettingsData.Instance._InputCancel;
        _InputForward = SettingsData.Instance._InputForward;
        _InputBack = SettingsData.Instance._InputBack;

        _UpToJump = SettingsData.Instance._UpToJump;
        GameObject[] controlsMenuItems = GameObject.FindGameObjectsWithTag("ControlsMenu"); // Puts all controls menu objects in a list.
        int index = 0;
        while (index <= controlsMenuItems.Length - 1) // Repeats for every game object.
        {
            //Debug.Log("Refreshing Visual of " + controlsMenuItems[index],controlsMenuItems[index]);
            controlsMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
    }

    public void ResetToDefault(SettingsButtonData data) // Resets the keybind to the default bind.
    {
        buttonText = data._TextMesh;
        switch(data._SettingID){ // This is a fancy if statement that only checks for the next item if the previous when was false.
            case  0:
                _InputLeft = data._DefaultBind;
                break;
            case  1:
                _InputRight = data._DefaultBind;
                break;
            case  2:
                _InputUp = data._DefaultBind;
                break;
            case  3:
                _InputDown = data._DefaultBind;
                break;
            case  4:
                _InputJump = data._DefaultBind;
                break;
            case  5:
                _InputSprint = data._DefaultBind;
                break;
            case  6:
                _InputAttack = data._DefaultBind;
                break;
            case  7:
                _InputParry = data._DefaultBind;
                break;
            case  8:
                _InputInteract = data._DefaultBind;
                break;
            case  9:
                _InputMenuLeft = data._DefaultBind;
                break;
            case  10:
                _InputMenuRight = data._DefaultBind;
                break;
            case  11:
                _InputMenuUp = data._DefaultBind;
                break;
            case  12:
                _InputMenuDown = data._DefaultBind;
                break;
            case  13:
                _InputSubmit = data._DefaultBind;
                break;
            case  14:
                _InputCancel = data._DefaultBind;
                break;
            case  15:
                _InputForward = data._DefaultBind;
                break;
            case  16:
                _InputBack = data._DefaultBind;
                break;
        }
        buttonText.text = "" + data._DefaultBind;
    }
    
    public void OnGUI() // Runs basically any time this is active just less often than update but still runs when neccessary.
    {
        Event e = Event.current;
        if ((e.isKey || e.isMouse) && e.keyCode != KeyCode.None && e.keyCode != KeyCode.Return && e.keyCode != KeyCode.Escape)
        {
            currentKeyDown = e.keyCode;
            //Debug.Log(e.keyCode);
        }
    }

}
