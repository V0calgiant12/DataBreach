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
    [Header("References")]
    [SerializeField] private int characterNum;
    [SerializeField] private TextMeshProUGUI text;
    public TextBoxAnimation textBox;
    [SerializeField] private GameObject prefab;
    public static TextWrite Instance;

    private int frame = 0;
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
        if(GameObject.Find("Player").GetComponent<PlayerStateManager>() != null)
        {
            if(PlayerStateManager.Instance.playerData.interacting == false)
            {
                text.text = "";
            }
        }
        else if (!IntroCutsceneManager.Instance.textIsOpen)
        {
            text.text = "";
        }
    }
    public void WriteText(TextData data)
    {
        _TextInput = data._TextInput;
        _TextSound = data._TextSound;
        _TextSpeed = data._TextSpeed;
        frame = 0;
        StartCoroutine(Write());
    }

    IEnumerator Write()
    {
        textBox.Open();
        _Writing = true;
        while(frame < 40) // Delay before beginning to write.
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
                if(!char.IsWhiteSpace(_TextInput[characterNum]) && char.ToString(_TextInput[characterNum]) != "<" && char.ToString(_TextInput[characterNum]) != ">")
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
                    waitTime = _TextSpeed * 3;
                }
                output += _TextInput[characterNum];
                characterNum += 1;
                if (Input.GetKey(SettingsData.Instance._InputInteract))
                {
                    output = _TextInput;
                    characterNum = _TextInput.Length;
                }
                text.text = output;
            }
            waitTime -= Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        _Writing = false;
        if(GameObject.Find("Player").GetComponent<PlayerStateManager>() != null)
        {
            StartCoroutine(ReadyToClose());
        }
    }
    private IEnumerator ReadyToClose()
    {
        while (textBox.open)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && _Writing == false)
            {
                PlayerStateManager.Instance.playerData.interacting = false;
                Close();
                PlayerStateManager.Instance.SwitchState(PlayerStateManager.Instance.IdleState);
            }
            yield return null;
        }
    }
}