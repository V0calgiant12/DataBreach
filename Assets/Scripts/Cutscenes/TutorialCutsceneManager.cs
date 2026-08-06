using System.Collections;
using UnityEngine;

public class TutorialCutsceneManager : MonoBehaviour
{
    
    [Header("Cutscene Manager References:")]
    public GameObject InvisbleWall;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator screen;
    [SerializeField] private MusicManager music;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource alarm;
    [SerializeField] private AudioClip shake1;
    [SerializeField] private AudioClip shake2;
    [SerializeField] private AudioClip rapidExplosion;
    [SerializeField] private int currentScene;
    public bool textIsOpen;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStateManager.Instance.Interact();
            ProgressCutscene();
            music.FadeOutCaller(60);
        }
    }
    private void ProgressCutscene()
    {
        currentScene += 1;
        switch (currentScene)
        {
            case(1):
                InvisbleWall.SetActive(true);
                TriggerShake.Instance.BurstShake(1,1,false);
                audioSource.clip = shake1;
                audioSource.Play();
                StartCoroutine(WaitForFrames(60));
                break;
            case(2):
                TriggerShake.Instance.BurstShake(1.5f,1.25f,false);
                audioSource.clip = shake1;
                audioSource.Play();
                StartCoroutine(WaitForFrames(30));
                break;
            case(3):
                TriggerShake.Instance.BurstShake(2f,1.5f,false);
                audioSource.clip = shake1;
                audioSource.Play();
                StartCoroutine(WaitForFrames(150));
                break;
            case(4):
                PlayerStateManager.Instance.Interact();
                TriggerShake.Instance.BurstShake(4,2,false);
                anim.SetInteger("Scene", 4);
                alarm.Play();
                audioSource.clip = shake2;
                audioSource.Play();
                StartCoroutine(WaitUntilTextCloses(120,60,false));
                break;
            case(5):
                PlayerStateManager.Instance.Interact();
                anim.SetInteger("Scene", 5);
                audioSource.clip = rapidExplosion;
                audioSource.Play();
                TriggerShake.Instance.Shake(90,5);
                break;
        }
    }
    private IEnumerator WaitUntilTextCloses(int delay, int delay2, bool allowMovementAfter)
    {
        int elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        TextWrite.Instance.WriteText(GetComponent<TextData>());
        yield return new WaitUntil(() => !TextWrite.Instance._Writing && (UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack));
        TextWrite.Instance.Close();
        textIsOpen = false;
        if (!allowMovementAfter)
        {
            PlayerStateManager.Instance.Interact();
        }
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator WaitForFrames(int frames)
    {
        int timer = frames;
        Debug.Log(timer);
        while (timer > 0)
        {
            timer -= Time.timeScale == 1 ? 1 : 0;
            Debug.Log(timer);
            yield return null;
        }
        ProgressCutscene();
    }
}