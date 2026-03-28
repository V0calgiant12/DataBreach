using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class TextWrite : MonoBehaviour
{
    [Header("Perameters")]
    public string _TextInput;
    public int _TextSpeed;
    [Header("References")]
    [SerializeField] private int characterNum;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject textBox;
    public TextWrite Instance;
    void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract))
        {
            StartCoroutine(WriteText(_TextInput,5));
        }
    }

    IEnumerator WriteText(string inputText, int speed)
    {
        characterNum = 0;
        string output = "";
        while(characterNum < inputText.Length)
        {
            output += inputText[characterNum];
            characterNum += 1;
            text.text = output;

            yield return new WaitForFrames(speed);
        }
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