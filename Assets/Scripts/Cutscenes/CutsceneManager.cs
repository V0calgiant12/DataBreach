using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Manager References:")]
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private TextData textData;
    [SerializeField] private AudioClip bitSound;
    [SerializeField] private AudioClip playerSound;
    public bool walking;
    public static CutsceneManager Instance;

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
                anim.SetInteger("Scene",1); // Walking
                StartCoroutine(Walk1());
                break;
            case(2):
                anim.SetInteger("Scene",2); // See bit
                StartCoroutine(WaitForFrames(120));
                break;
            case(3):
                anim.SetInteger("Scene",3); // Bit isn't awake
                Text(1);
                break;
            case(4):
                anim.SetInteger("Scene",4); // Bit wakes up
                Text(2);
                break;
            case(5):
                anim.SetInteger("Scene",5); // Bit notices you
                Text(3);
                break;
            case(6):
                anim.SetInteger("Scene",6); // Bit says yes
                Text(4);
                break;
            case(7):
                anim.SetInteger("Scene",7); // Bit says yes
                StartCoroutine(WaitForFrames(60*3));
                break;
            case(8):
                anim.SetInteger("Scene",8); // Bit says yes
                StartCoroutine(Walk2());
                Text(5);
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
                textData._TextInput = "It doesn't seem like it's awake yet...";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(30,60));
                break;
            case(2):
                textData._TextInput = "It's flying around in a panic! It seems like something bad may have happened...";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(200,30));
                break;
            case(3):
                textData._TextInput = "It seems like it's trying to ask you for something, so you ask if it needs help.";
                textData._TextSpeed = 3;
                textData._TextSound = bitSound;
                StartCoroutine(WaitUntilTextCloses(240,60));
                break;
            case(4):
                textData._TextInput = "You agree to help it. Since you were already heading East, you ask if the problem is that way.";
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
    private IEnumerator Walk1()
    {
        walking = true;
        float elapsed = 0;
        while (walking)
        {
            inputTimer = -500;
            playerRb.linearVelocityX = 8;
            yield return null;
        }
        elapsed = 0;
        while (elapsed != 60)
        {
            inputTimer = -500;
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
            inputTimer = -500;
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
            inputTimer = -500;
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
            inputTimer = -500;
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