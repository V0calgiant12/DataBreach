using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Call FadeOutCaller() and FadeInCaller() from other scripts. Input speed in fames.
    /// Set the active instruments with ChangeInstruments(). Input the count of how many will be active.
    /// </summary>
    [Header("References")]
    [SerializeField] private MusicChild[] children;
    [SerializeField] private int instrumentsActive;
    void Start()
    {
        ChangeInstruments(instrumentsActive);
        FadeInCaller(120);
    }
    public void FadeOutCaller(int speed)
    {
        StartCoroutine(FadeOut(speed));
    }
    public void FadeInCaller(int speed)
    {
        StartCoroutine(FadeIn(speed));
    }
    public void ChangeInstruments(int count)
    {
        instrumentsActive = count;
        StartCoroutine(SetInstruments(count));
    }

    private IEnumerator FadeOut(int speed)
    {
        int index = 0;
        while (index <= children.Length-1)
        {
            children[index].FadeOutCaller(speed);
            index += 1;
            yield return null;
        }
    }
    private IEnumerator FadeIn(int speed)
    {
        int index = 0;
        while (index <= children.Length-1)
        {
            children[index].FadeInCaller(speed);
            index += 1;
            yield return null;
        }
    }
    private IEnumerator SetInstruments(int amount)
    {
        int index = 0;
        while (index < amount)
        {
            children[index].Activate();
            index += 1;
            yield return null;
        }
        while (index <= children.Length-1)
        {
            children[index].Deactivate();
            index += 1;
            yield return null;
        }
    }
}
