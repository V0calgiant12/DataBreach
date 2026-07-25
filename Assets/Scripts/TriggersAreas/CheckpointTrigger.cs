using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStateManager.Instance.playerData.lastCheckpoint = transform.position;
    }
}