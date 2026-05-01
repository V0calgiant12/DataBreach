using UnityEngine;

public class ForceManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public void AddForce(float xForce, float yForce, bool ifPlayer)
    {
        if(ifPlayer)
        {
            Debug.Log("adding force");
            rb.linearVelocity = new Vector2(xForce, yForce);
        }
        else
        {
            Debug.Log("filler");
        }
    }
}