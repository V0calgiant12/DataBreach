using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private GameObject particle;
    bool used;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!used && PlayerStateManager.Instance.playerData.lastCheckpoint != new Vector2(transform.position.x, transform.position.y))
            {
                //used = true;
                Vector2 playerPos = PlayerStateManager.Instance.transform.position;
                audioSource.PlaySound(checkpointSound,1,1,0);
                Instantiate(particle,new Vector2(playerPos.x,playerPos.y+1),transform.rotation);
                PlayerStateManager.Instance.playerData.lastCheckpoint = transform.position;
            }
        }
    }
}