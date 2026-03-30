using UnityEngine;

public class HealthHud : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void OnGUI()
    {
        animator.SetInteger("PlayerHealth", PlayerStateManager.Instance.playerData.playerHealth);
    }
}