using System;
using Unity.VisualScripting;
using UnityEngine;

public class TextData : MonoBehaviour
{
    [Header("Interaction Settings:")]
    public string[] _TextPageInput;
    public int _TextSpeed;
    public bool _DelayBetweenLines;
    [Header("Decisions")]
    public bool _DecisionAfterText;
    public bool _UsesHeartCoin;
    public string[] _DecisionOptions;
    public string[] _PostDecisionTextDecision1;
    public string[] _PostDecisionTextDecision2;
    [Header("Interaction References:")]
    public AudioClip _TextSound;
    public void ChangeText(int pageNumber, string newText, bool clearPages)
    {
        if (clearPages)
        {
            Array.Clear(_TextPageInput,0,1);
        }
        Array.Resize(ref _TextPageInput,pageNumber+1);
        _TextPageInput[pageNumber] = newText;
    }
    public void TrimPages(int numberOfPages)
    {
        for(int i = numberOfPages; i < numberOfPages; i++)
        {
            _TextPageInput[i] = "";
        }
        Array.Resize(ref _TextPageInput,numberOfPages);
    }
}