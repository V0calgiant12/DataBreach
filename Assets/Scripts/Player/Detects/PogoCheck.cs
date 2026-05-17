using Unity.VisualScripting;
using UnityEngine;

public class PogoCheck : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(playerData.anim.GetInteger("attackId") == 4)
        {
            Debug.Log("Adding Force",other.gameObject);
            playerData.PlayerRb.linearVelocity = new Vector2(playerData.PlayerRb.linearVelocityX,20f);
        }
    }
}