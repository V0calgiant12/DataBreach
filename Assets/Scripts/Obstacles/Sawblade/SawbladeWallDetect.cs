using UnityEngine;
using System.Collections;

public class SawbladeWallDetect : MonoBehaviour
{
    [Header("Sawblade Settings (For going down):")]
    public float downDistance = -.75f;
    [Header("Sawblade References:")]
    public Sawblade SawbladeRef;
    public GameObject WallDetectLeft;
    public GameObject WallDetectRight;
    public GameObject SawbladeGameObject;
    [SerializeField] private float elapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WallDetectLeft = SawbladeRef.WallDetectLeft;
        WallDetectRight = SawbladeRef.WallDetectRight;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("Spikes") || other.gameObject.CompareTag("Stone"))
        {
            Debug.Log("Saw ground test");
            StartCoroutine(SawbladeRef.SawbladeDown());
        }
    }
}
