using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutsceneManager : MonoBehaviour
{
    [Header("Cutscene Manager References:")]
    [SerializeField] private Animator anim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private SceneTransition sceneTransition;
    public Rigidbody2D playerRb;
    [SerializeField] private TextData textData;
    [SerializeField] private AudioSource fallingWind;
    [SerializeField] private AudioClip bitSound;
    [SerializeField] private AudioClip playerSound;
    public bool falling;
    public static IntroCutsceneManager Instance;

    private int inputTimer;
    [SerializeField] private int currentScene;
    public int maxScene;
    public bool textIsOpen;
    void Start()
    {
        Instance = this;
        inputTimer = 0;
        HideInputMethod();
        ProgressCutscene();
    }
    void Update()
    {
        if ((UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack) && inputTimer > 100)
        {
            HideInputMethod();
            inputTimer = 0;
        }
        else
        {
            inputTimer += 1;
        }
        if(inputTimer == 600)
        {
            ShowInputMethod();
        }

        //fallingWind.pitch = 6/playerRb.linearVelocityY-3;
    }
    private void ShowInputMethod()
    {
        inputText.text = "Press " + SettingsData.Instance._InputInteract + ".";
    }
    private void HideInputMethod()
    {
        inputText.text = "";
    }
    public void ProgressCutscene()
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
    private void Text(int number)
    {
        textIsOpen = true;
        switch (number)
        {
            case(1):
                textData._TextInput = "The bit doesn't seem to be anywhere around here...";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(30,60));
                break;
            case(2):
                textData._TextInput = "Maybe it's further up ahead.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(200,30));
                break;
            case(3):
                textData._TextInput = "Let's hope this new world wasn't affected by whatever happened before...";
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
            inputTimer = -100;
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
            inputTimer = -100;
            elapsed += 1;
            yield return null;
        }
        elapsed = 0;
        while (elapsed != 500)
        {
            inputTimer = -100;
            elapsed += 1;
            yield return null;
        }
        playerAnim.SetTrigger("standUp");
        playerAnim.SetBool("lookAround",true);
        elapsed = 0;
        while (elapsed != 260)
        {
            inputTimer = -100;
            elapsed += 1;
            yield return null;
        }
        // lookAround should technically be here but has to be above because of timing.
        elapsed = 0;
        while (elapsed != 90)
        {
            inputTimer = -100;
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
            inputTimer = -100;
            playerRb.linearVelocityX = 8;
            elapsed += 1;
            yield return null;
        }
        anim.SetTrigger("FadeOut");
    }
    private IEnumerator WaitUntilTextCloses(int delay, int delay2)
    {
        int elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += 1;
            inputTimer = -100;
            yield return null;
        }
        TextWrite.Instance.WriteText(textData);
        yield return new WaitUntil(() => !TextWrite.Instance._Writing && (UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack));
        TextWrite.Instance.Close();
        textIsOpen = false;
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += 1;
            inputTimer = -100;
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