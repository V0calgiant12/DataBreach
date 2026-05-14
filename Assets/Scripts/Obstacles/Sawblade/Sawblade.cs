using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Sawblade : MonoBehaviour
{
    [Header("Sawblade Settings:")]
    public float sawbladeTime = 1f;
    public float activeDistance = 10f;
    public float upDistance = .85f;
    public enum LeftRight
    {
        Left,
        Right,
    }
    public LeftRight SawbladeDirection;
    [Header("Sawblade References:")]
    [SerializeField] private GameObject sprite;
    public GameObject DetectPlayerLeft;
    public GameObject DetectPlayerRight;
    public GameObject SawbladeHitbox;
    public Rigidbody2D sawbladeRb;
    public AudioSource SawbladeAudioSource;
    public float SawbladeSpinVolume;
    private bool playerDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Depending on the sawblades dircection its set to, it will set the opposite player detector to false
        if (SawbladeDirection == LeftRight.Right)
        {
            DetectPlayerLeft.SetActive(false);
        }
        if (SawbladeDirection == LeftRight.Left)
        {
            DetectPlayerRight.SetActive(false);
        }
        sawbladeRb = GetComponent<Rigidbody2D>();
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        sprite.transform.Rotate(0,0,5f * ((SawbladeDirection == LeftRight.Right) ? -1f : 1f));
        // Sets the sounds volume to the audio sources volume
        SawbladeAudioSource.volume = SawbladeSpinVolume;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the player isnt already detected and the player goes into the player detector trigger, it will set the player detects to false, play the sawblade cutting sound on loop, and start a coroutine
        if (other.gameObject.CompareTag("Player") && (!playerDetected))
        {
            DetectPlayerLeft.SetActive(false);
            DetectPlayerRight.SetActive(false);
            SawbladeAudioSource.Play();
            StartCoroutine(Move());
            StartCoroutine(SawbladeUp());
        }

    }
    private IEnumerator SawbladeUp()
    {
        float startingY = transform.localPosition.y;
        // elapsed is a timer that goes until the sawblade goes the correct y height
        float distance = 0;
        // SawbladeSpinVolume is for fading in and out the volume over time
        SawbladeSpinVolume = 0f;
        while (upDistance > distance)
        {
            SawbladeSpinVolume += 0.05f;
            distance += 0.1f;
            transform.localPosition = new Vector2(transform.localPosition.x, startingY + distance*sawbladeTime);
            yield return null;
        }
        playerDetected = true;
    }
    private IEnumerator Move()
    {
        float startX = transform.localPosition.x;
        if(SawbladeDirection == LeftRight.Right)
        {
            while(transform.localPosition.x < (activeDistance - (.15 * activeDistance/sawbladeTime)) * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f) + startX)
            {
                sawbladeRb.linearVelocity = new Vector2(activeDistance/sawbladeTime * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f),sawbladeRb.linearVelocityY);
                yield return null;
            }
        }
        if(SawbladeDirection == LeftRight.Left)
        {
            while(transform.localPosition.x > (activeDistance - (.15 * activeDistance/sawbladeTime)) * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f) + startX)
            {
                sawbladeRb.linearVelocity = new Vector2(activeDistance/sawbladeTime * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f),sawbladeRb.linearVelocityY);
                yield return null;
            }
        }
        StartCoroutine(SawbladeDown());
    }
    public IEnumerator SawbladeDown()
    {
        Debug.Log("Starting Down");
        float startingY = transform.localPosition.y;
        // elapsed is a timer that goes until the sawblade goes the correct y height
        float distance = 0;
        while (upDistance > distance)
        {
            Debug.Log("Going Down");
            distance += 0.1f;
            SawbladeSpinVolume -= 0.05f;
            transform.localPosition = new Vector2(transform.localPosition.x, startingY - distance*sawbladeTime);
            //Debug.Log(sawbladeRb.linearVelocityY + elapsed);
            yield return null;
        }
        Destroy(gameObject);
        Debug.Log("Down");
    }
}