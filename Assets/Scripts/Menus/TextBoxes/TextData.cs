using System;
using Unity.VisualScripting;
using UnityEngine;

public class TextData : MonoBehaviour
{
    [Header("Interaction Settings:")]
    public string[] _TextPageInput;
    public int _TextSpeed;
    public bool _DelayBetweenLines;
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
}