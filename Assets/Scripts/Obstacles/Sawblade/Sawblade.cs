using UnityEngine;
using System.Collections;

public class Sawblade : MonoBehaviour
{
    [Header("Sawblade Settings:")]
    public float sawbladeSpeed = 10f;
    public float upDistance = .85f;
    public enum LeftRight
    {
        Left,
        Right,
    }
    public LeftRight SawbladeDirection;
    [Header("Sawblade References:")]
    public GameObject WallDetectLeft;
    public GameObject WallDetectRight;
    public GameObject DetectPlayerLeft;
    public GameObject DetectPlayerRight;
    public GameObject SawbladeHitbox;
    public Rigidbody2D sawbladeRb;
    public Vector2 SawbladeVelocity;
    [SerializeField] private float elapsed;
    private bool playerDetected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SawbladeDirection == LeftRight.Right)
        {
            DetectPlayerLeft.SetActive(false);
        }
        if (SawbladeDirection == LeftRight.Left)
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
        if (playerDetected)
        {
            if (SawbladeDirection == LeftRight.Right)
            {
                SawbladeVelocity = new Vector2(sawbladeSpeed, 0);
            }
            if (SawbladeDirection == LeftRight.Left)
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
    private IEnumerator SawbladeUp()
    {
        elapsed = 0;
        while (upDistance > elapsed)
        {
            elapsed += 0.05f;
            if (SawbladeDirection == LeftRight.Right)
            {
                SawbladeVelocity =  new Vector2(sawbladeSpeed, sawbladeRb.linearVelocityY + elapsed);
            }
            if (SawbladeDirection == LeftRight.Left)
            {
                SawbladeVelocity =  new Vector2(-sawbladeSpeed, sawbladeRb.linearVelocityY + elapsed);
            }
            //Debug.Log(sawbladeRb.linearVelocityY + elapsed);
            yield return null;
        }
        if (SawbladeDirection == LeftRight.Right)
        {
            SawbladeVelocity = new Vector2(sawbladeSpeed, 0);
        }
        if (SawbladeDirection == LeftRight.Left)
        {
            SawbladeVelocity = new Vector2(-sawbladeSpeed, 0);
        }
        playerDetected = true;
        WallDetectLeft.SetActive(true);
        WallDetectRight.SetActive(true);
        Debug.Log("Done going up");
    }
}