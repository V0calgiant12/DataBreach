using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class OreTermiteManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyPoximityTrigger warning;
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip otReady;
    [SerializeField] private AudioClip otAttack;
    [SerializeField] private AudioClip otCancel;

    [Header("Stats")]
    public bool extended;
    public bool playerInTrigger;
    void Start()
    {
        
    }
    void Update()
    {
        if (animator.GetInteger("Stage") == 0)
        {
            extended = false;
        }
        if (warning.trigger && warning.playerDetected)
        {
            if (!extended)
            {
                audioSource.PlaySound(otReady,1,Random.Range(0.9f,1.1f),1);
            }
            animator.SetBool("Prepare",true);
            warning.trigger = false;
        }
        if (warning.trigger && !warning.playerDetected)
        {
            if (!extended)
            {
                audioSource.PlaySound(otCancel,1,Random.Range(0.9f,1.1f),1);
            }
            animator.SetBool("Prepare",false);
            warning.trigger = false;
        }
    }
    public void Stab()
    {
        audioSource.PlaySound(otAttack,1,1,1);
        extended = true;
        animator.SetInteger("Stage",1);
        StartCoroutine(Retract());
    }
    private IEnumerator Retract()
    {
        int elapsed = 0;
        while(extended == true)
        {
            elapsed += Time.timeScale == 1 ? 1:0;
            if(elapsed >= 100 && !playerInTrigger)
            {
                animator.SetInteger("Stage",3);
            }
            yield return null;
        }
    }
}