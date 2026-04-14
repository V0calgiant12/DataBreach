using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour
{
    // If this is false, then it will go left
    // If this is true, then it will go right
    // Set this to whatever direction you need it to go before using it
    public bool sawbladeDirection;
    public bool playerDetected;
    public float sawbladeSpeed = 10f;
    public float upDistance = .75f;
    private float elapsed;
    public GameObject WallDetectLeft;
    public GameObject WallDetectRight;
    public GameObject DetectPlayerLeft;
    public GameObject DetectPlayerRight;
    public GameObject SawbladeHitbox;
    public Rigidbody2D sawbladeRb;
    public Vector2 SawbladeVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sawbladeDirection)
        {
            DetectPlayerLeft.SetActive(false);
        }
        if (!sawbladeDirection)
        {
            DetectPlayerRight.SetActive(false);
        }
        WallDetectLeft.SetActive(false);
        WallDetectRight.SetActive(false);
        sawbladeRb = GetComponent<Rigidbody2D>();
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        sawbladeRb.linearVelocity = SawbladeVelocity;
        // I plan on making it so that it starts in the ground and then pops up when the player in in a certain range (probably like double whatever slimes chase range is)
        // You would also have to be around the same y-level as it for it to trigger
        // After detecting a wall within a few tiles, depending if its going right or left, it will slowly go back into the ground (Slowly but not too slow for it to end up hitting the wall) and be deleted
        // (or we could make it go back and forth but I personally think we shouldn't do that)
        if (playerDetected)
        {
            if (sawbladeDirection)
            {
                SawbladeVelocity = new Vector2(sawbladeSpeed, 0);
            }
            if (!sawbladeDirection)
            {
                SawbladeVelocity = new Vector2(-sawbladeSpeed, 0);
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (!playerDetected))
        {
            Debug.Log("Player Detected");
            DetectPlayerLeft.SetActive(false);
            DetectPlayerRight.SetActive(false);
            StartCoroutine(SawbladeUp());
        }

    }
    public IEnumerator SawbladeUp()
    {
        elapsed = 0;
        while (upDistance > elapsed)
        {
            elapsed += 0.05f;
            if (sawbladeDirection)
            {
                SawbladeVelocity =  new Vector2(sawbladeSpeed, sawbladeRb.linearVelocityY + elapsed);
            }
            if (!sawbladeDirection)
            {
                SawbladeVelocity =  new Vector2(-sawbladeSpeed, sawbladeRb.linearVelocityY + elapsed);
            }
            //Debug.Log(sawbladeRb.linearVelocityY + elapsed);
            yield return null;
        }
        if (sawbladeDirection)
        {
            SawbladeVelocity = new Vector2(sawbladeSpeed, 0);
        }
        if (!sawbladeDirection)
        {
            SawbladeVelocity = new Vector2(-sawbladeSpeed, 0);
        }
        playerDetected = true;
        WallDetectLeft.SetActive(true);
        WallDetectRight.SetActive(true);
        Debug.Log("Done going up");
    }
}