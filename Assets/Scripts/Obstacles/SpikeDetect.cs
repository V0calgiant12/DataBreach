using UnityEngine;
using System.Collections;
using System.Runtime.Versioning;

public class SpikeDetect : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float noMoving = 0.25f;
    public float xLaunch;
    public float yLaunch;
    private int frame;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            if (!PlayerStateManager.Instance.playerData.leftOrRight)
            {
                xLaunch = Random.Range(5, 15);
            }
            else
            {
                xLaunch = Random.Range(-5, -15);
            }
            yLaunch = Random.Range(12, 20);
            PlayerStateManager.Instance.playerData.movementAllowed = false;
            StartCoroutine(noMovement(noMoving));
        }
    }
    private IEnumerator noMovement(float noMoving)
    {
        frame = 0;
        yield return new WaitUntil(() => frame >= 1);
        PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.playerHealth - 1f;
        Debug.Log(PlayerStateManager.Instance.playerData.playerHealth);
        PlayerRb.linearVelocity = new Vector2(xLaunch, yLaunch);
        yield return new WaitUntil(() => GroundCheck.Instance._IsGrounded);
        PlayerStateManager.Instance.playerData.movementAllowed = true;
    }
    void Update()
    {
        frame += 1;
    }
}
