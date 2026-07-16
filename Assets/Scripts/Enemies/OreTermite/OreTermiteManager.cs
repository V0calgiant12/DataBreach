using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class OreTermiteManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [Header("Stats")]
    public bool extended;
    public bool playerInTrigger;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Stab()
    {
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
                extended = false;
            }
            yield return null;
        }
    }
}