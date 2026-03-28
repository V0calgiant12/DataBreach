using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;
using UnityEngine.TextCore.Text;
using Unity.Collections;

public class TextWrite : MonoBehaviour
{
    [Header("Perameters")]
    public string _TextInput;
    public int _TextSpeed;
    public AudioClip _TextSound;
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
    }
    public void Close()
    {
        text.text = "";
        textBox.Close();
    }
    void Update()
    {
        if(PlayerStateManager.Instance.playerData.interacting == false)
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
        if(frame < 10)
        {
            characterNum = 0;
            string output = "";
            while(characterNum < _TextInput.Length)
            {
                output += _TextInput[characterNum];
                text.text = output;
                if(char.ToString(_TextInput[characterNum]) != " ")
                {
                    GameObject audioClone = Instantiate(prefab);
                    audioClone.GetComponent<MenuAudioSource>().TextSound(this);
                }
                characterNum += 1;

                yield return new WaitForFrames(_TextSpeed);
            }
        }
        frame += 1;
    }
}
public class WaitForFrames : CustomYieldInstruction
    {
        private int _targetFrameCount;

        public WaitForFrames(int numberOfFrames)
        {
            _targetFrameCount = Time.frameCount + numberOfFrames;
        }

        public override bool keepWaiting
        {
            get
            {
                return Time.frameCount < _targetFrameCount;
            }
        }
    }