using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using UnityEngine.TextCore.Text;
using Unity.Collections;
using Unity.VisualScripting;
using System.Timers;

public class TextWrite : MonoBehaviour
{
    [Header("Perameters")]
    public string _TextInput;
    public int _TextSpeed;
    public AudioClip _TextSound;
    public bool _Writing;
    public bool _DelayBetweenLines;
    private int pageNumber;
    private int maxPages;
    [Header("References")]
    [SerializeField] private int characterNum;
    [SerializeField] private TextMeshProUGUI text;
    public TextBoxAnimation textBox;
    [SerializeField] private GameObject prefab;
    public static TextWrite Instance;
    private TextData storedData;

    private int frame = 0;
    private int inputBuffer = 0;
    void Start()
    {
        Instance = this;
        text.text = "";
    }
    public void Close()
    {
        text.text = "";
        textBox.Close();
    }
    void Update()
    {
        if ((UserInput.Instance.KeyDownInteract || UserInput.Instance.KeyDownAttack) && Time.timeScale == 1 && _Writing)
        {
            inputBuffer = 20;
        }
        if(GameObject.Find("Player").GetComponent<PlayerStateManager>() != null)
        {
            if(PlayerStateManager.Instance.playerData.interacting == false)
            {
                text.text = "";
            }
        }
        else if (!GeneralCutsceneManager.Instance.textIsOpen)
        {
            text.text = "";
        }

        inputBuffer -= Time.timeScale == 1 ? 1:0;
    }
    public void WriteText(TextData data)
    {
        pageNumber = 0;
        maxPages = data._TextPageInput.Length-1;
        if(pageNumber > maxPages || data._TextPageInput[pageNumber] == null)
        {
            _TextInput = "ERROR: NO TEXT DATA FOR PAGE " + pageNumber + ".";
        }
        else
        {
            _TextInput = data._TextPageInput[pageNumber];
        }
        _TextSound = data._TextSound;
        _TextSpeed = data._TextSpeed;
        _DelayBetweenLines = data._DelayBetweenLines;
        storedData = data;
        frame = 0;
        StartCoroutine(Write(true));
    }
    private void WriteNextPage()
    {
        text.text = "";
        pageNumber += 1;
        if(storedData._TextPageInput[pageNumber] == null)
        {
            _TextInput = "ERROR: NO TEXT DATA FOR PAGE " + pageNumber + ".";
        }
        else
        {
            _TextInput = storedData._TextPageInput[pageNumber];
        }
        _TextSound = storedData._TextSound;
        _TextSpeed = storedData._TextSpeed;
        _DelayBetweenLines = storedData._DelayBetweenLines;
        frame = 0;
        StartCoroutine(Write(false));
    }

    IEnumerator Write(bool delay)
    {
        textBox.Open();
        _Writing = true;
        while(frame < 1) // Always a 1 frame delay no matter what.
        {
            frame += 1;
            yield return null;
        }
        inputBuffer = 0;
        while(frame < 40 && delay) // Delay before beginning to write.
        {
            frame += 1;
            yield return null;
        }
        characterNum = 0;
        int waitTime = 0;
        string output = "";
        while(characterNum < _TextInput.Length)
        {
            if(waitTime == 0)
            {
                waitTime = _TextSpeed;
                if(char.ToString(_TextInput[characterNum]) == "." || char.ToString(_TextInput[characterNum]) == "," || char.ToString(_TextInput[characterNum]) == ";" || char.ToString(_TextInput[characterNum]) == "?" || char.ToString(_TextInput[characterNum]) == "!")
                {
                    waitTime = _TextSpeed * 3;
                }
                if(!char.IsWhiteSpace(_TextInput[characterNum]) && char.ToString(_TextInput[characterNum]) != "<" && char.ToString(_TextInput[characterNum]) != ">" && char.ToString(_TextInput[characterNum]) != "'" && char.ToString(_TextInput[characterNum]) != "\"")
                {
                    GameObject audioClone = Instantiate(prefab);
                    audioClone.GetComponent<MenuAudioSource>().TextSound(this);
                }
                if(char.ToString(_TextInput[characterNum]) == "<" && char.ToString(_TextInput[characterNum+1]) == "b" && char.ToString(_TextInput[characterNum+2]) == "r" && char.ToString(_TextInput[characterNum+3]) == ">")
                {
                    output += _TextInput[characterNum];
                    characterNum += 1;
                    output += _TextInput[characterNum];
                    characterNum += 1;
                    output += _TextInput[characterNum];
                    characterNum += 1;
                    output += _TextInput[characterNum];
                    characterNum += 1;
                    waitTime = _TextSpeed * (_DelayBetweenLines ? 10 : 3);
                }
                else
                {
                    output += _TextInput[characterNum];
                    characterNum += 1;
                }
                if (inputBuffer > 0)
                {
                    inputBuffer = 0;
                    output = _TextInput;
                    characterNum = _TextInput.Length;
                }
                text.text = output;
            }
            waitTime -= Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        _Writing = false;
        while (textBox.open)
        {
            if ((UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack) && _Writing == false && Time.timeScale == 1)
            {
                if(pageNumber >= maxPages)
                {
                    if(GameObject.Find("Player").GetComponent<PlayerStateManager>() != null)
                    {
                        PlayerStateManager.Instance.playerData.interacting = false;
                        PlayerStateManager.Instance.SwitchState(PlayerStateManager.Instance.IdleState);
                    }
                    Close();
                }
                else
                {
                    WriteNextPage();
                }
            }
            yield return null;
        }
    }
}