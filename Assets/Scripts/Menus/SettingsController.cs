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

public class SettingsController : MonoBehaviour
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
    public void GetName(string name)
    {
        objectName = name;
        Debug.Log(objectName);
    }
    public void ListenForKey(SettingsButtonData data)
    {
        buttonText = data._TextMesh;
        StartCoroutine(StartListeningForKey(data._SettingID));
    }
    private IEnumerator StartListeningForKey(int inputNumber)
    {
        buttonText.text = "Press any key...";
        yield return new WaitUntil(() => Input.anyKeyDown);
        switch(inputNumber){
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
    public void ToggleSetting(string settingName)
    {
        if(settingName == "UpToJump")
        {
            _UpToJump = !_UpToJump;
        }
    }
    
    public void OnGUI()
    {
        Event e = Event.current;
        if ((e.isKey || e.isMouse) && e.keyCode != KeyCode.None && e.keyCode != KeyCode.Return && e.keyCode != KeyCode.Escape)
        {
            currentKeyDown = e.keyCode;
            //Debug.Log(e.keyCode);
        }
    }
}
