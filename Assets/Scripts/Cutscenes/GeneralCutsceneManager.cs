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
    public int maxScene;
    public bool textIsOpen;
    public bool falling;

    void Start()
    {
        Instance = this;
    }
    
    public IEnumerator Walk(float distance,bool fade)
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
            ProgressCutscene();
        }
    }
    public IEnumerator Sprint(float distance,bool fade)
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
    }
    public abstract void ProgressCutscene();
}