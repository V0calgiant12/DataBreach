using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TextWrite : MonoBehaviour
{
    [Header("Perameters")]
    public string _TextInput;
    public string[] _TextPageInput;
    public int _TextSpeed;
    public AudioClip _TextSound;
    public AudioClip _HeartSound;
    public bool _Writing;
    public bool _WaitingForDecision;
    public bool _DelayBetweenLines;
    public bool _DecisionAfterText;
    private int pageNumber;
    private int maxPages;
    [Header("References")]
    [SerializeField] private int characterNum;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI option1Text;
    [SerializeField] private TextMeshProUGUI option2Text;
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
        _TextPageInput = data._TextPageInput;
        if(pageNumber > maxPages || _TextPageInput[pageNumber] == null)
        {
            _TextInput = "ERROR: NO TEXT DATA FOR PAGE " + pageNumber + ".";
        }
        else
        {
            _TextInput = _TextPageInput[pageNumber];
        }
        _TextSound = data._TextSound;
        _TextSpeed = data._TextSpeed;
        _DelayBetweenLines = data._DelayBetweenLines;
        _DecisionAfterText = data._DecisionAfterText;
        storedData = data;
        frame = 0;
        StartCoroutine(Write(true));
    }
    private void WriteNextPage()
    {
        text.text = "";
        pageNumber += 1;
        maxPages = _TextPageInput.Length-1;
        if(_TextPageInput[pageNumber] == null)
        {
            _TextInput = "ERROR: NO TEXT DATA FOR PAGE " + pageNumber + ".";
        }
        else
        {
            _TextInput = _TextPageInput[pageNumber];
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
        if(pageNumber >= maxPages && _DecisionAfterText)
        {
            _WaitingForDecision = true;
            textBox.OptionsUp();
            yield return new WaitForFrames(1);
            option1Text.text = storedData._DecisionOptions[0];
            option2Text.text = storedData._DecisionOptions[1];
        }

        while (textBox.open)
        {
            if ((UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack) && _Writing == false && Time.timeScale == 1 && !_WaitingForDecision)
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
    public void Option(int optionNumber)
    {
        switch (optionNumber)
        {
            case(1):
                Array.Resize(ref _TextPageInput,storedData._TextPageInput.Length + storedData._PostDecisionTextDecision1.Length);
                for(int i = _TextPageInput.Length-storedData._PostDecisionTextDecision1.Length; i < _TextPageInput.Length; i++)
                {
                    _TextPageInput[i] = storedData._PostDecisionTextDecision1[i-storedData._TextPageInput.Length];
                }
                break;
            case(2):
                Array.Resize(ref _TextPageInput,storedData._TextPageInput.Length + storedData._PostDecisionTextDecision2.Length);
                for(int i = _TextPageInput.Length-storedData._PostDecisionTextDecision2.Length; i < _TextPageInput.Length; i++)
                {
                    //Debug.Log(i + ", " + (i-storedData._TextPageInput.Length) + ", " + storedData._PostDecisionTextDecision2.Length + ", " + (storedData._TextPageInput.Length + storedData._PostDecisionTextDecision2.Length-1));
                    _TextPageInput[i] = storedData._PostDecisionTextDecision2[i-storedData._TextPageInput.Length];
                }
                if (storedData._UsesHeartCoin)
                {
                    UseHeartCoin();
                }
                break;
        }
        _DecisionAfterText = false;
        _WaitingForDecision = false;
        textBox.OptionsDown();
        WriteNextPage();
        
    }
    private void UseHeartCoin()
    {
        GameObject audioClone = Instantiate(prefab);
        audioClone.GetComponent<MenuAudioSource>().HeartSound(_HeartSound,0.9f);
        PlayerStateManager.Instance.playerData.maxHealth += 1;
        PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.maxHealth;
        PlayerStateManager.Instance.playerData.hasHeartCoin = false;
        PlayerStateManager.Instance.playerData.heartCoinSaved = false;
        HeartCoinIconHandler.Instance.UpdateGUI();
    }
}