using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;
using System;
using Mono.Cecil.Cil;
using UnityEngine.InputSystem;

public class SettingsController : MonoBehaviour
{
    public KeyCode _InputLeft = KeyCode.LeftArrow; // 0
    public KeyCode _InputRight = KeyCode.RightArrow; // 1
    public KeyCode _InputUp = KeyCode.UpArrow; // 2
    public KeyCode _InputDown = KeyCode.DownArrow; // 3
    public KeyCode _InputJump = KeyCode.Space; // 4
    public KeyCode _InputSprint = KeyCode.X; // 5
    public KeyCode _InputAttack = KeyCode.Z; // 6
    public KeyCode _InputInteract = KeyCode.C; // 7
    private KeyCode currentKeyDown;
    public void ListenForKey(int inputNumber)
    {
        StartCoroutine(StartListeningForKey(inputNumber));
    }
    private IEnumerator StartListeningForKey(int inputNumber)
    {
        yield return new WaitUntil(() => Input.anyKeyDown);
        if (inputNumber == 0)
        {
            _InputLeft = currentKeyDown;            
        }
        else if (inputNumber == 1)
        {
            _InputRight = currentKeyDown;
        }
        else if (inputNumber == 2)
        {
            _InputUp = currentKeyDown;
        }
        else if (inputNumber == 3)
        {
            _InputDown = currentKeyDown;
        }
        else if (inputNumber == 4)
        {
            _InputJump = currentKeyDown;
        }
        else if (inputNumber == 5)
        {
            _InputSprint = currentKeyDown;
        }
        else if (inputNumber == 6)
        {
            _InputAttack = currentKeyDown;
        }
        else if (inputNumber == 7)
        {
            _InputInteract = currentKeyDown;
        }
    }
    
    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None)
        {
            currentKeyDown = e.keyCode;
            Debug.Log(e.keyCode);
        }
    }
}
