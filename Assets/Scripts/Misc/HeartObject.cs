using UnityEngine;
using UnityEngine.Audio;

public class HeartObject : MonoBehaviour
{
    [Header("Heart Powerup Settings:")]
    public float bobbingSpeed = 2f;
    public float bobbingHeight = 0.5f;
    [Header("Heart Powerup References:")]
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip heartObtainSound;
    private Vector2 startPos;
    void Start()
    {
        // Gets the heart's starting position
        startPos = transform.position;
    }
    void Update()
    {
        // Fancy math function for the powerup to bob up and down
        float newY = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight + startPos.y;
        transform.position = new Vector2(startPos.x, newY);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If the player has less than full health and touches the powerup, then it uses it
        if(other.gameObject.CompareTag("Player") && PlayerStateManager.Instance.playerData.playerHealth < 5)
        {
            audioSource.HeartSound(heartObtainSound);
            PlayerStateManager.Instance.playerData.playerHealth += 1;
            Debug.Log("Health up by 1, health is now " + PlayerStateManager.Instance.playerData.playerHealth);
            Destroy(gameObject);
        }
    }
}
// [Heart shaped object]