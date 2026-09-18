using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BitCutsceneManager : GeneralCutsceneManager
{

    void Start()
    {
        Instance = this;
        automaticCamera = true;
        ProgressCutscene();
    }
    void Update()
    {

        //fallingWind.pitch = 6/playerRb.linearVelocityY-3;
    }
    public override void ProgressCutscene()
    {
        currentScene += 1;
        switch (currentScene)
        {
            case(1):
                StartCoroutine(Walk(20,false,60));
                break;
            case(2):
                cameraLoc = new Vector2(21f,0);
                automaticCamera = false;
                StartCoroutine(WaitForFrames(30));
                break;
            case(3):
                StartCoroutine(Sprint(28,false,60));
                break;
            case(4):
                playerAnim.SetTrigger("help");
                StartCoroutine(WaitForFrames(120));
                break;
            case(5):
                anim.SetInteger("Scene",5);
                StartCoroutine(WaitForFrames(150));
                break;
            case(6):
                Text(1);
                break;
            case(7):
                playerAnim.SetTrigger("pet");
                anim.SetInteger("Scene",7);
                Text(2);
                break;
            case(8):
                playerAnim.SetTrigger("pet");
                anim.SetInteger("Scene",8);
                StartCoroutine(WaitForFrames(160));
                break;
            case(9):
                automaticCamera = true;
                StartCoroutine(Walk(50,true,30));
                Text(3);
                break;
            case(10):
                EndCutscene();
                break;
        }
    }
    private void EndCutscene()
    {
        sceneTransition.TransitionToScene(5,2);
    }
    public override void Text(int number)
    {
        textIsOpen = true;
        switch (number)
        {
            case(1):
                textData.ChangeText(0,"You woke up the bit!",true);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(0,30));
                break;
            case(2):
                textData.ChangeText(0,"It seems very happy to see you.",true);
                textData.ChangeText(1,"It almost feels like it's humming.",false);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(120,60));
                break;
            case(3):
                textData.ChangeText(0,"Now that you have reunited with your bit, you have a new goal, find a way to get back to the system.",true);
                textData.ChangeText(1,"But there's no telling what you'll run into on your way there.",false);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(45,60));
                break;
        }
    }
}