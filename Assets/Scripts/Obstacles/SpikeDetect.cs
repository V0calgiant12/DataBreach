using UnityEngine;
using System.Collections;

public class SpikeDetect : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float noMoving = 0.25f;
    public float xLaunch;
    public float yLaunch;
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
                xLaunch = Random.Range(5, 10);
            }
            else
            {
                xLaunch = Random.Range(-5, -10);
            }
            yLaunch = Random.Range(12, 20);
            PlayerStateManager.Instance.playerData.movementAllowed = false;
            StartCoroutine(noMovement(noMoving));
        }
    }
    private IEnumerator noMovement(float noMoving)
    {
        PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.playerHealth - 1f;
        Debug.Log(PlayerStateManager.Instance.playerData.playerHealth);
        PlayerRb.linearVelocity = new Vector2(xLaunch, yLaunch);
        yield return new WaitForSeconds(noMoving);
        PlayerStateManager.Instance.playerData.movementAllowed = true;
    }
}