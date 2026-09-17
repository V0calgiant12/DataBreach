using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutsceneManager : GeneralCutsceneManager
{
    [Header("Intro Cutscene Manager References:")]
    [SerializeField] private AudioSource fallingWind;
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
                playerRb.linearVelocityY = -30;
                StartCoroutine(Fall1());
                break;
            case(2):
                Text(1);
                break;
            case(3):
                playerAnim.SetBool("lookAround",false);
                Text(2);
                break;
            case(4):
                StartCoroutine(Walk());
                Text(3);
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
    private IEnumerator Fall1()
    {
        falling = true;
        float elapsed = 0;
        while (falling)
        {
            yield return null;
        }
        playerAnim.SetInteger("attackId",10);
        playerAnim.SetBool("attacking",true);
        elapsed = 0;
        float playerFallingVelocity = 0;
        while (elapsed != 85)
        {
            if(elapsed == 26)
            {
                playerFallingVelocity = playerRb.linearVelocityY;
                fallingWind.Stop();
            }
            if(27 <= elapsed && elapsed <= 62)
            {
                playerRb.linearVelocityY = 0;
            }
            if(elapsed == 27)
            {
                anim.SetTrigger("Break");
            }
            if(63 <= elapsed && elapsed <= 70)
            {
                playerRb.linearVelocityY = playerFallingVelocity;
            }
            elapsed += 1;
            yield return null;
        }
        elapsed = 0;
        while (elapsed != 500)
        {
            elapsed += 1;
            yield return null;
        }
        playerAnim.SetTrigger("standUp");
        playerAnim.SetBool("lookAround",true);
        elapsed = 0;
        while (elapsed != 260)
        {
            elapsed += 1;
            yield return null;
        }
        // lookAround should technically be here but has to be above because of timing.
        elapsed = 0;
        while (elapsed != 90)
        {
            elapsed += 1;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator Walk()
    {
        int elapsed = 0;
        while (elapsed != 400)
        {
            playerRb.linearVelocityX = 8;
            elapsed += 1;
            yield return null;
        }
        anim.SetTrigger("FadeOut");
    }
    
}