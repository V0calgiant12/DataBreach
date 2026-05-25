using System.Collections;
using UnityEngine;

public class TutorialCutsceneManager : MonoBehaviour
{
    
    [Header("Cutscene Manager References:")]
    [SerializeField] private Animator anim;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shake1;
    [SerializeField] private AudioClip shake2;
    [SerializeField] private int currentScene;
    public bool textIsOpen;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStateManager.Instance.Interact();
        }
    }
    private void ProgressCutscene()
    {
        currentScene += 1;
        switch (currentScene)
        {
            case(1):
                CameraShaker.Instance.BurstShake(1,1);
                WaitForFrames(60);
                break;
            case(2):
                CameraShaker.Instance.BurstShake(1.5f,1);
                WaitForFrames(30);
                break;
            case(3):
                CameraShaker.Instance.BurstShake(2f,1);
                WaitForFrames(150);
                break;
            case(4):
                CameraShaker.Instance.BurstShake(4,1);
                anim.SetInteger("Scene", 4);
                WaitUntilTextCloses(120,60);
                break;
            case(5):
                anim.SetInteger("Scene", 5);
                break;
        }
    }
    private IEnumerator WaitUntilTextCloses(int delay, int delay2)
    {
        int elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        TextWrite.Instance.WriteText(GetComponent<TextData>());
        yield return new WaitUntil(() => !TextWrite.Instance._Writing && Input.GetKeyDown(SettingsData.Instance._InputInteract));
        TextWrite.Instance.Close();
        textIsOpen = false;
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
        while (timer > 0)
        {
            timer -= Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        ProgressCutscene();
    }
}