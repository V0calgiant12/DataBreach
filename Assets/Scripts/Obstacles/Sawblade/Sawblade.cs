using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Sawblade : MonoBehaviour
{
    [Header("Sawblade Settings:")]
    public int sawbladeTime = 60;
    public float activeDistance = 10f;
    public float upDistance = 1f;
    public int upTime = 20;
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
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && (!playerDetected))
        {
            DetectPlayerLeft.SetActive(false);
            DetectPlayerRight.SetActive(false);
            SawbladeAudioSource.Play();
            //StartCoroutine(Move());
            StartCoroutine(SawbladeUp());
        }

    }
    private IEnumerator SawbladeUp()
    {
        float startingY = transform.localPosition.y;
        // SawbladeSpinVolume is for fading in and out the volume over time
        int elapsed = 0;
        bool moving = false;
        SawbladeSpinVolume = 0f;
        while (upDistance > transform.localPosition.y)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            SawbladeSpinVolume += Time.timeScale == 1 ? 0.05f : 0;
            transform.localPosition = new Vector2(transform.localPosition.x, upDistance/upTime * elapsed + startingY); // d=r/t, r=d*t
            //Debug.Log("UpTime: " + upTime + " UpDist: " + upDistance + " Elapsed: " + elapsed + " StartingY: " + startingY + " Combined: " + (upDistance/upTime * elapsed + startingY));
            if(transform.localPosition.y >= upDistance / 2 && !moving)
            {
                moving = true;
                StartCoroutine(Move());
            }
            yield return null;
        }
        playerDetected = true;
    }
    private IEnumerator Move()
    {
        float startX = transform.localPosition.x;
        int elapsedM = 0;
        while(elapsedM < sawbladeTime - upTime/2)
        {
            elapsedM += Time.timeScale == 1 ? 1 : 0;
            transform.localPosition = new Vector2(activeDistance/sawbladeTime * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f)* elapsedM + startX,transform.localPosition.y);
            yield return null;
        }
        StartCoroutine(SawbladeDown());
        while(elapsedM < sawbladeTime)
        {
            elapsedM += Time.timeScale == 1 ? 1 : 0;
            transform.localPosition = new Vector2(activeDistance/sawbladeTime * ((SawbladeDirection == LeftRight.Right) ? 1f : -1f)* elapsedM + startX,transform.localPosition.y);
            yield return null;
        }
        
    }
    public IEnumerator SawbladeDown()
    {
        Debug.Log("Starting Down");
        float startingY = transform.localPosition.y;
        int elapsed = 0;
        while (0 < transform.localPosition.y)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            SawbladeSpinVolume -= Time.timeScale == 1 ? -0.05f : 0;
            transform.localPosition = new Vector2(transform.localPosition.x, -1 * upDistance/upTime * elapsed + startingY);
            //Debug.Log("UpTime: " + upTime + " UpDist: " + upDistance + " Elapsed: " + elapsed + " StartingY: " + startingY + " Combined: " + (-1*upDistance/upTime * elapsed + startingY));
            yield return null;
        }
        Destroy(gameObject);
    }
}