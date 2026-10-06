using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HeartObject : MonoBehaviour
{
    [Header("Stats:")]
    public int restorationAmount = 1;
    [Header("Heart Powerup References:")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip heartObtainSound;
    [SerializeField] private Animator anim;
    private bool pickedUp = false;
    void Start()
    {
        playerData.pickUpHeart = false;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If the player has less than full health and touches the powerup, then it uses it
        if(other.gameObject.CompareTag("Player") && playerData.playerHealth < playerData.maxHealth && !pickedUp)
        {
            playerData.pickUpHeart = true;
            playerData.resetVelocity = true;
            audioSource.HeartSound(heartObtainSound);
            playerData.playerHealth += restorationAmount;
            if(playerData.maxHealth < playerData.playerHealth)
            {
                playerData.playerHealth = playerData.maxHealth;
            }
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