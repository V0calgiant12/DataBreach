using UnityEngine;

public class AirGustPush : MonoBehaviour
{
    public GroundCheck GroundCheckRef;
    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("test", other.gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("test");
            Debug.Log(other.gameObject.GetComponent<ForceManager>());
            other.gameObject.GetComponent<ForceManager>().AddForce(0f, 5f, true);
        }
    }
}
