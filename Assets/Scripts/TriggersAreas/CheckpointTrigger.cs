using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private ParticleSpawner particleSpawner;
    bool used;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!used && PlayerStateManager.Instance.playerData.lastCheckpoint != new Vector2(transform.position.x, transform.position.y))
            {
                //used = true;
                Vector2 playerPos = PlayerStateManager.Instance.transform.position;
                audioSource.PlaySound(checkpointSound,1,1,0,1,transform.position);
                particleSpawner.SpawnParticle("Checkpoint", new Vector2(playerPos.x,playerPos.y+1));
                PlayerStateManager.Instance.playerData.lastCheckpoint = transform.position;
                PlayerStateManager.Instance.playerData.heartCoinSaved = PlayerStateManager.Instance.playerData.hasHeartCoin;
            }
        }
    }
}