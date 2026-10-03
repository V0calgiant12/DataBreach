using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HeartObject : MonoBehaviour
{
    [Header("Heart Powerup References:")]
    [SerializeField] private PlayerData PlayerDataRef;
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip heartObtainSound;
    [SerializeField] private Animator anim;
    private bool pickedUp = false;
    private Vector2 startPos;
    void Start()
    {
        PlayerDataRef.pickUpHeart = false;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If the player has less than full health and touches the powerup, then it uses it
        if(other.gameObject.CompareTag("Player") && PlayerStateManager.Instance.playerData.playerHealth < PlayerStateManager.Instance.playerData.maxHealth && !pickedUp)
        {
            PlayerDataRef.pickUpHeart = true;
            PlayerDataRef.resetVelocity = true;
            audioSource.HeartSound(heartObtainSound);
            PlayerStateManager.Instance.playerData.playerHealth += 1;
            pickedUp = true;
            anim.SetTrigger("Collect");
            StartCoroutine(DestroyObject());
        }
    }
    private IEnumerator DestroyObject()
    {
        float elapsed = 0;
        while(elapsed < 0.75f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
// [Heart shaped object]
// ^ How long has that comment been there??? - V0cal, 9/24/2026