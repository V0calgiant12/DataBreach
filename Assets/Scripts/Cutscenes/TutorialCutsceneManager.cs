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
    [SerializeField] private EffectSound effectAS;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource alarm;
    [SerializeField] private AudioClip shake1;
    [SerializeField] private AudioClip shake2;
    [SerializeField] private AudioClip impact;
    [SerializeField] private AudioClip sparkHit;
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
                bit.StartCoroutine(bit.GlideTo(new Vector2(transform.position.x+0.3438f,transform.position.y-3.5f)));
                StartCoroutine(WaitForSeconds(1.5f,true,true));
                music.FadeOutCaller(1);
                StartCoroutine(Explosion(1,1,0.5f,shake1));
                break;
            case(2):
                bit.enabled = false;
                anim.SetInteger("Scene",2);
                StartCoroutine(Explosion(1.5f,1.5f,0.75f,shake1));
                StartCoroutine(Explosion(2,2,1.25f,shake1));
                StartCoroutine(WaitForSeconds(3,true,true));
                break;
            case(3):
                anim.SetInteger("Scene",3);
                alarm.Play();
                StartCoroutine(BitFallSound(1.05f));
                StartCoroutine(BitFallSound(1.5f));
                StartCoroutine(Explosion(5,2,0.667f,shake2));
                StartCoroutine(WaitForSeconds(2,true,true));
                break;
            case(4):
                PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.SprintRight,4);
                anim.SetInteger("Scene",4);
                StartCoroutine(PlayerLook());
                break;
            case(5):
                anim.SetInteger("Scene",5);
                StartCoroutine(PlayerJump());
                break;
            case(6):
                PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.ShortJumpRight,2.5f);
                break;
        }
    }
    private IEnumerator Explosion(float magnitude, float lengthMult, float delay, AudioClip audioClip)
    {
        float elapsed = 0;
        while(elapsed < delay)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        TriggerShake.Instance.BurstShake(magnitude,lengthMult,false,0);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    private IEnumerator BitFallSound(float delay)
    {
        float elapsed = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        effectAS.PlaySound(impact,1,1.7f,1,1,effectAS.transform.position);
        effectAS.PlaySound(sparkHit,0.8f,1.7f,1,1,effectAS.transform.position);
    }
    private IEnumerator PlayerLook()
    {
        float elapsed = 0;
        while (elapsed < 1.5f)
        {
            if(elapsed >= 0.5f)
            {
                PlayerStateManager.Instance.PlayerFlash(3,3.75f);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(WaitUntilTextCloses(0,0.5f,false));
        while (currentScene == 4)
        {
            PlayerStateManager.Instance.PlayerFlash(3,2.767f);
            yield return null;
        }
    }
    private IEnumerator PlayerJump()
    {
        float elapsed = 0;
        while (elapsed < 2.783f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        ProgressCutscene();
    }
    public void PlayFootstep()
    {
        PlayerStateManager.Instance.audioSource.PlaySound(PlayerStateManager.Instance._StoneWalk,1f,Random.Range(0.6f,1.3f),0,1,PlayerStateManager.Instance.transform.position);
    }
    private IEnumerator WaitUntilTextCloses(float delay, float delay2, bool allowMovementAfter)
    {
        float elapsed = 0;
        while(delay > elapsed)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        TextWrite.Instance.WriteText(GetComponent<TextData>());
        yield return new WaitUntil(() => !TextWrite.Instance._Writing && (UserInput.Instance.KeyDownInteract||UserInput.Instance.KeyDownAttack));
        if (!allowMovementAfter)
        {
            PlayerStateManager.Instance.Interact(PlayerStateManager.InteractControls.Stop,0);
        }
        elapsed = 0;
        while(delay2 > elapsed)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        ProgressCutscene();
    }
    private IEnumerator WaitForSeconds(float seconds,bool animState,bool progress)
    {
        float timer = seconds;
        //Debug.Log(timer);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);
            yield return null;
        }
        anim.enabled = animState;
        if (progress)
        {
            ProgressCutscene();
        }
    }
}
//            case(2):
//                music.FadeOutCaller(60);
//                InvisbleWall.SetActive(true);
//                TriggerShake.Instance.BurstShake(1,1,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForSeconds(60));
//                break;
//            case(3):
//                TriggerShake.Instance.BurstShake(1.5f,1.25f,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForSeconds(30));
//                break;
//            case(4):
//                TriggerShake.Instance.BurstShake(2f,1.5f,false,0);
//                audioSource.clip = shake1;
//                audioSource.Play();
//                StartCoroutine(WaitForSeconds(150));
//                break;