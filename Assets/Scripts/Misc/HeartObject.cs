using UnityEngine;
using UnityEngine.Audio;

public class HeartObject : MonoBehaviour
{
    public float bobbingSpeed = 2f;
    public float bobbingHeight = 0.5f;
    private Vector2 startPos;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // fancy math function 
        float newY = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight + startPos.y;
        transform.position = new Vector2(startPos.x, newY);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") && PlayerStateManager.Instance.playerData.playerHealth < 5)
        {
            Debug.Log(audioSource);
            audioSource.Play();
            PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.playerHealth + 1;
            Debug.Log("Health up by 1, health is now " + PlayerStateManager.Instance.playerData.playerHealth);
            Destroy(gameObject);
        }
    }
}
// [Heart shaped object]