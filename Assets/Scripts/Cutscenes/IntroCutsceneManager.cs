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
                StartCoroutine(WaitUntilTextCloses(0.5f,1f));
                break;
            case(2):
                textData.ChangeText(0,"It needs to be found quickly, who knows what could have happened to it by now.",true);
                textData.ChangeText(1,"It must be further ahead.",false);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(3.333f,0.5f));
                break;
            case(3):
                textData.ChangeText(0,"Without knowing what caused whatever happened up there, there's no telling what could've happened down here.<br>This world could end up being extremely dangerous.",true);
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(4f,1f));
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
        { // Falling Sequence
            // Wait
            while (elapsed <= 0.433f)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Get Falling Velocity and stop the wind sound.
            playerFallingVelocity = playerRb.linearVelocityY;
            fallingWind.Stop();

            // Wait one more "frame"
            while (elapsed <= 0.45f)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Start Break animation
            anim.SetTrigger("Break");

            // Set the player velocity to 0 between when the animation starts and when it ends.
            while (elapsed < 1.05f)
            {
                playerRb.linearVelocityY = 0;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Set the player velocity back
            while (elapsed < 1.167f)
            {
                playerRb.linearVelocityY = playerFallingVelocity;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Wait
            while (elapsed < 1.417f)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        elapsed = 0;
        while (elapsed < 8.333f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerAnim.SetTrigger("standUp");
        playerAnim.SetBool("lookAround",true);
        elapsed = 0;
        while (elapsed < 4.333f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        // lookAround should technically be here but has to be above because of timing.
        elapsed = 0;
        while (elapsed < 1.5f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator Walk()
    {
        float elapsed = 0;
        while (elapsed < 6.667f)
        {
            playerRb.linearVelocityX = 8;
            elapsed += Time.deltaTime;
            yield return null;
        }
        anim.SetTrigger("FadeOut");
    }
    
}