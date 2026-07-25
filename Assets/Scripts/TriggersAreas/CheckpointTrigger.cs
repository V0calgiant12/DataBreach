using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    bool used;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!used && PlayerStateManager.Instance.playerData.lastCheckpoint != new Vector2(transform.position.x, transform.position.y))
            {
                //used = true;
                Instantiate(particle,PlayerStateManager.Instance.transform.position,transform.rotation);
                PlayerStateManager.Instance.playerData.lastCheckpoint = transform.position;
            }
        }
    }
}