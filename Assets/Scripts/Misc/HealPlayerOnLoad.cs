using UnityEngine;

public class HealPlayerOnLoad : MonoBehaviour
{
    void Start()
    {
        PlayerStateManager.Instance.playerData.playerHealth = 5;
    }
}