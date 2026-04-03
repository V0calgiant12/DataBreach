using UnityEngine;
using System.Collections;

public class SawbladeWallDetect : MonoBehaviour
{
    public Sawblade SawbladeRef;
    public GameObject WallDetectLeft;
    public GameObject WallDetectRight;
    public GameObject Sawblade;
    private float elapsed;
    public float downDistance = -1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WallDetectLeft = SawbladeRef.WallDetectLeft;
        WallDetectRight = SawbladeRef.WallDetectRight;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Saw ground test");
            StartCoroutine(SawbladeDown());
        }
    }
    public IEnumerator SawbladeDown()
    {
        elapsed = 0;
        while (downDistance < elapsed)
        {
            elapsed -= 0.05f;
            Debug.Log(elapsed);
            if (SawbladeRef.sawbladeDirection)
            {
                SawbladeRef.SawbladeVelocity =  new Vector2(SawbladeRef.sawbladeSpeed, SawbladeRef.sawbladeRb.linearVelocityY + elapsed);
            }
            if (!SawbladeRef.sawbladeDirection)
            {
                SawbladeRef.SawbladeVelocity =  new Vector2(-SawbladeRef.sawbladeSpeed, SawbladeRef.sawbladeRb.linearVelocityY + elapsed);
            }
            yield return null;
            Destroy(Sawblade);
        }
    }
}
