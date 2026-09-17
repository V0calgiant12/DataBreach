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
                StartCoroutine(Walk(20,false));
                break;
            case(2):
                StartCoroutine(WaitForFrames(60));
                break;
            case(3):
                anim.SetInteger("Scene", 3);
                StartCoroutine(WaitForFrames(30));
                break;
            case(4):
                StartCoroutine(Sprint(20,false));
                break;
            case(5):
                EndCutscene();
                break;
        }
    }
    private void EndCutscene()
    {
        sceneTransition.TransitionToScene(2,2);
    }
    public override void Text(int number)
    {
        textIsOpen = true;
        switch (number)
        {
            case(1):
                textData.ChangeText(0,"The bit is no where in sight.",true);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(30,60));
                break;
            case(2):
                textData.ChangeText(0,"It needs to be found quickly, who knows what could have happened to it by now.",true);
                textData.ChangeText(1,"It must be further ahead.",false);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(200,30));
                break;
            case(3):
                textData.ChangeText(0,"Without knowing what caused whatever happened up there, there's no telling what could've happened down here.<br>This world could end up being extremely dangerous.",true);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(240,60));
                break;
        }
    }
}