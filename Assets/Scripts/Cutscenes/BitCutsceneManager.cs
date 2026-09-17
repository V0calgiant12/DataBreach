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
                Walk(35,false);
                break;
            case(2):
                
                break;
            case(3):
                
                break;
            case(4):
                
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
    private void Text(int number)
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
    private IEnumerator WaitUntilTextCloses(int delay, int delay2)
    {
        int elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        TextWrite.Instance.WriteText(GetComponent<TextData>());
        yield return new WaitUntil(() => !TextWrite.Instance.textBox.open);
        textIsOpen = false;
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator WaitForFrames(int frames)
    {
        yield return new WaitForFrames(frames);
        ProgressCutscene();
    }
}