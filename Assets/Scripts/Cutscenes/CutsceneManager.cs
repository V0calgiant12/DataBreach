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
    
    private int inputTimer;
    [SerializeField] private int currentScene;
    public int maxScene;
    void Start()
    {
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
            if(currentScene != maxScene)
            {
                ProgressCutscene();
            }
            else
            {
                EndCutscene();
            }
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
                anim.SetInteger("Scene",1);
                StartCoroutine(Walk1());
                break;
            case(2):
                anim.SetInteger("Scene",2);
                break;
        }
    }
    private void EndCutscene()
    {
        sceneTransition.TransitionToScene(1,2);
    }
    private IEnumerator Walk1()
    {
        int elapsed = 0;
        while (elapsed != 60*5)
        {
            inputTimer = -500;
            playerRb.linearVelocityX = 8;
            elapsed += 1;
            yield return null;
        }
        elapsed = 0;
        while (elapsed != 45)
        {
            inputTimer = -500;
            elapsed += 1;
            yield return null;
        }
        ProgressCutscene();
    }
}