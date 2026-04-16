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
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Stone"))
        {
            Debug.Log("Saw ground test");
            StartCoroutine(SawbladeDown());
        }
    }
    private IEnumerator SawbladeDown()
    {
        elapsed = 0;
        while (downDistance < elapsed)
        {
            elapsed -= 0.05f;
            Debug.Log(elapsed);
            if (SawbladeRef.SawbladeDirection == Sawblade.LeftRight.Right)
            {
                SawbladeRef.SawbladeVelocity =  new Vector2(SawbladeRef.sawbladeSpeed, SawbladeRef.sawbladeRb.linearVelocityY + elapsed);
            }
            if (SawbladeRef.SawbladeDirection == Sawblade.LeftRight.Left)
            {
                SawbladeRef.SawbladeVelocity =  new Vector2(-SawbladeRef.sawbladeSpeed, SawbladeRef.sawbladeRb.linearVelocityY + elapsed);
            }
            yield return null;
        }
        Destroy(SawbladeGameObject);
    }
}
