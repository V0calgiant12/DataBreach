using System.Collections;
using UnityEngine;

public class HealthHud : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator.SetBool("SceneStarted", false);
        animator.SetInteger("PlayerHealth", PlayerStateManager.Instance.playerData.playerHealth);
        animator.SetInteger("MaxHealth", PlayerStateManager.Instance.playerData.maxHealth);
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        int elapsed = 0;
        while(elapsed <= 2)
        {
            elapsed += 1;
            yield return null;
        }
        animator.SetBool("SceneStarted", true);
    }
    void OnGUI()
    {
        animator.SetInteger("PlayerHealth", PlayerStateManager.Instance.playerData.playerHealth);
        animator.SetInteger("MaxHealth", PlayerStateManager.Instance.playerData.maxHealth);
    }
}