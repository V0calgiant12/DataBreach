using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;
using System;
using Mono.Cecil.Cil;
using UnityEngine.InputSystem;
using TMPro;
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
    public bool _UpToJump = false;
    private KeyCode currentKeyDown;
    private string objectName;
    [SerializeField] private TextMeshProUGUI buttonText;
    void Start() // Refreshes settings on load.
    {
        RefreshSettings();
    }
    public void GetName(string name) // Gets object name. Im actually not entirely sure if this code still runs anywhere but it's staying just incase it does.
    {
        objectName = name;
        //Debug.Log(objectName);
    }
    public void ListenForKey(SettingsButtonData data) // Initial function called when a button is pressed for rebinding. Has button data that is unique to each button.
    {
        buttonText = data._TextMesh;
        StartCoroutine(StartListeningForKey(data._SettingID));
    }
    private IEnumerator StartListeningForKey(int inputNumber) // Listens for the next key to be pressed and acts accordingly when it does.
    {
        buttonText.text = "Press any key...";
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
        }
        buttonText.text = "" + currentKeyDown;
    }
    public void ToggleSetting(SettingsToggleData data) // Handles when a setting is toggled
    {
        switch(data._ToggleID)
        {
            case(0):
                _UpToJump = !_UpToJump;
                break;
        }
    }
    private void RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "ControlsMenu" to refresh their visuals, which is handled elsewhere.
    {
        _InputLeft = SettingsData.Instance._InputLeft;
        _InputRight = SettingsData.Instance._InputRight;
        _InputUp = SettingsData.Instance._InputUp;
        _InputDown = SettingsData.Instance._InputDown;
        _InputJump = SettingsData.Instance._InputJump;
        _InputSprint = SettingsData.Instance._InputSprint;
        _InputAttack = SettingsData.Instance._InputAttack;
        _InputParry = SettingsData.Instance._InputParry;
        _InputInteract = SettingsData.Instance._InputInteract;

        _UpToJump = SettingsData.Instance._UpToJump;
        GameObject[] controlsMenuItems = GameObject.FindGameObjectsWithTag("ControlsMenu");
        int index = 0;
        while (index <= 9)
        {
            controlsMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
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
