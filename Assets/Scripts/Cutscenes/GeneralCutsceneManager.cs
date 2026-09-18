using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public abstract class GeneralCutsceneManager : MonoBehaviour 
{
    [Header("Cutscene Manager References:")]
    public Animator anim;
    public Animator playerAnim;
    public Rigidbody2D playerRb;
    public SceneTransition sceneTransition;
    public TextData textData;
    public AudioClip bitSound;
    public AudioClip playerSound;
    public static GeneralCutsceneManager Instance;
    public int currentScene;
    public bool textIsOpen;
    public bool falling;
    public bool automaticCamera = true;
    public Vector2 cameraLoc = new Vector2(0,0);
    public enum CutsceneType
    {
        Intro,
        Bit
    }
    public CutsceneType cutscene;

    void Start()
    {
        Instance = this;
    }
    
    public IEnumerator Walk(float distance,bool fade,int delay)
    {
        float startX = playerRb.transform.position.x;
        while (playerRb.transform.position.x < startX + distance)
        {
            playerRb.linearVelocityX = 8;
            yield return null;
        }
        if (fade)
        {
            anim.SetTrigger("FadeOut");
        }
        else
        {
            int elapsed = 0;
            while(elapsed < delay)
            {
                elapsed += Time.timeScale == 1 ? 1:0;
                yield return null;
            }
            ProgressCutscene();
        }
    }
    public IEnumerator Sprint(float distance,bool fade,int delay)
    {
        float startX = playerRb.transform.position.x;
        while (playerRb.transform.position.x < startX + distance)
        {
            playerRb.linearVelocityX = 15;
            yield return null;
        }
        if (fade)
        {
            anim.SetTrigger("FadeOut");
        }
        else
        {
            int elapsed = 0;
            while(elapsed < delay)
            {
                elapsed += Time.timeScale == 1 ? 1:0;
                yield return null;
            }
            ProgressCutscene();
        }
    }
    public abstract void ProgressCutscene();
    public abstract void Text(int number);
    public IEnumerator WaitUntilTextCloses(int delay, int delay2)
    {
        int elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        TextWrite.Instance.WriteText(GetComponent<TextData>());
        yield return new WaitUntil(() => !TextWrite.Instance.textBox.open);
        textIsOpen = false;
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        ProgressCutscene();
    }
    public IEnumerator WaitForFrames(int frames)
    {
        yield return new WaitForFrames(frames);
        ProgressCutscene();
    }
}