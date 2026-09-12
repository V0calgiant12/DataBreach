using System.Collections;
using UnityEngine;

public class TutorialCutsceneManager : MonoBehaviour
{
    
    [Header("Cutscene Manager References:")]
    public GameObject InvisbleWall;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator screen;
    [SerializeField] private BitAI bit;
    [SerializeField] private MusicManager music;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource alarm;
    [SerializeField] private AudioClip shake1;
    [SerializeField] private AudioClip shake2;
    [SerializeField] private AudioClip rapidExplosion;
    [SerializeField] private int currentScene;
    public bool textIsOpen;
    void Start()
    {
        anim.enabled = false;
        bit = GetComponentInChildren<BitAI>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentScene = 0;
            PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.Stop,0);
            ProgressCutscene();
        }
    }
    private void ProgressCutscene()
    {
        currentScene += 1;
        switch (currentScene)
        {
            case(1):
                bit.StartCoroutine(bit.GlideTo(new Vector2(transform.position.x,transform.position.y-3.5f)));
                StartCoroutine(WaitForFrames(90,true));
                break;
            case(2):
                bit.enabled = false;
                anim.SetInteger("Scene",2);
                StartCoroutine(WaitForFrames(180,true));
                break;
            case(3):
                anim.SetInteger("Scene",3);
                StartCoroutine(WaitForFrames(120,true));
                break;
            case(4):
                PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.SprintRight,4);
                StartCoroutine(WaitForFrames(180,true));
                break;
            case(5):
                PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.ShortJumpRight,3);
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
            PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.Stop,0);
        }
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator WaitForFrames(int frames,bool animState)
    {
        int timer = frames;
        //Debug.Log(timer);
        while (timer > 0)
        {
            timer -= Time.timeScale == 1 ? 1 : 0;
            //Debug.Log(timer);
            yield return null;
        }
        anim.enabled = animState;
        ProgressCutscene();
    }
}
//            case(2):
//                music.FadeOutCaller(60);
//                InvisbleWall.SetActive(true);
//                TriggerShake.Instance.BurstShake(1,1,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForFrames(60));
//                break;
//            case(3):
//                TriggerShake.Instance.BurstShake(1.5f,1.25f,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForFrames(30));
//                break;
//            case(4):
//                TriggerShake.Instance.BurstShake(2f,1.5f,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForFrames(150));
//                break;