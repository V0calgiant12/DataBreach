using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cutscene Manager References:")]
    [SerializeField] private Image panel1;
    [SerializeField] private Image panel2;
    [SerializeField] private Image panel3;
    [SerializeField] private Image panel4;
    [SerializeField] private Image panel1Color;
    [SerializeField] private Image panel2Color;
    [SerializeField] private Image panel3Color;
    [SerializeField] private Image panel4Color;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private SceneTransition sceneTransition;
    public Sprite[] sprite;
    public Sprite[] colorSprite;
    private int inputTimer;
    private int currentPanel;
    public int maxPanels;
    void Start()
    {
        inputTimer = 0;
        HideInputMethod();
        StartCoroutine(ProgressCutscene());
    }
    void Update()
    {
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && inputTimer > 100)
        {
            audioSource.Play();
            HideInputMethod();
            inputTimer = 0;
            if(currentPanel != maxPanels)
            {
                StartCoroutine(ProgressCutscene());
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
    private IEnumerator ProgressCutscene()
    {
        animator.SetInteger("Panel", currentPanel + 1);
        switch (currentPanel)
        {
            case(0):
                panel1.sprite = sprite[0];
                panel1Color.sprite = colorSprite[0];
                break;
            case(1):
                panel2.sprite = sprite[1];
                panel2Color.sprite = colorSprite[1];
                break;
            case(2):
                panel3.sprite = sprite[2];
                panel3Color.sprite = colorSprite[2];
                break;
            case(3):
                panel4.sprite = sprite[3];
                panel4Color.sprite = colorSprite[3];
                break;
            case(4):
                inputTimer = -75;
                yield return new WaitForSeconds(1.5f);
                
                panel1.sprite = sprite[4];
                panel1Color.sprite = colorSprite[4];
                break;
            case(5):
                panel2.sprite = sprite[5];
                panel2Color.sprite = colorSprite[5];
                break;
            case(6):
                panel3.sprite = sprite[6];
                panel3Color.sprite = colorSprite[6];
                break;
            case(7):
                panel4.sprite = sprite[7];
                panel4Color.sprite = colorSprite[7];
                break;
        }
        currentPanel += 1;
    }
    private void EndCutscene()
    {
        sceneTransition.TransitionToScene(1,2);
    }
}