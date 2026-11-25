using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate = 0;
    public TextMeshProUGUI display_Text;
    private int[] frames;
    private int i = 0;
    void OnEnable()
    {
        Array.Resize(ref frames,30);
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
    public void Update ()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        frames[i] = (int)current;
        i++;
        if(i == frames.Length)
        {
            float math = 0;
            for(int I = 0; I < frames.Length; I++)
            {
                math += frames[I];
            }
            math /= frames.Length;
            avgFrameRate = (int)math;
            i = 0;
        }
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}