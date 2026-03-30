using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Image panel1;
    [SerializeField] private Image panel2;
    [SerializeField] private Image panel3;
    [SerializeField] private Image panel4;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI inputText;
    [SerializeField] private SceneTransition sceneTransition;
    public Sprite[] sprite;
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
                break;
            case(1):
                panel2.sprite = sprite[1];
                break;
            case(2):
                panel3.sprite = sprite[2];
                break;
            case(3):
                panel4.sprite = sprite[3];
                break;
            case(4):
                inputTimer = -75;
                yield return new WaitForSeconds(1.5f);
                
                panel1.sprite = sprite[4];
                break;
            case(5):
                panel2.sprite = sprite[5];
                break;
            case(6):
                panel3.sprite = sprite[6];
                break;
            case(7):
                panel4.sprite = sprite[7];
                break;
        }
        currentPanel += 1;
    }
    private void EndCutscene()
    {
        sceneTransition.TransitionToScene(1,2);
    }
}