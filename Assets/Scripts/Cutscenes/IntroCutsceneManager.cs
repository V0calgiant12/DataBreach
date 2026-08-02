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
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && inputTimer > 100)
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
                break;
            case(3):
                break;
            case(4):
                break;
            case(5):
                break;
            case(6):
                break;
            case(7):
                break;
            case(8):
                break;
            case(9):
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
                textData._TextInput = "You found the bit, but it doesn't seem like it's awake yet...";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(30,60));
                break;
            case(2):
                textData._TextInput = "It woke up! It seems to be confused and panicked about where it is.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(200,30));
                break;
            case(3):
                textData._TextInput = "It seems glad to see you! You ask if it needs help getting back to where it came from.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(240,60));
                break;
            case(4):
                textData._TextInput = "You agree to help it and ask if the destination is to the East.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(240,60));
                break;
            case(5):
                textData._TextInput = "So the two of you set off on a journey. Maybe you can ask some locals what they know about the Eastern land of Vandros along your way.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(30,0));
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
            if(elapsed == 63)
            {
                playerRb.linearVelocityY = playerFallingVelocity;
            }
            inputTimer = -100;
            elapsed += 1;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator Walk2()
    {
        int elapsed = 0;
        while (elapsed != 500)
        {
            inputTimer = -100;
            playerRb.linearVelocityX = 8;
            elapsed += 1;
            yield return null;
        }
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
        yield return new WaitUntil(() => !TextWrite.Instance._Writing && Input.GetKeyDown(SettingsData.Instance._InputInteract));
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