using Unity.VisualScripting;
using UnityEngine;

public class GroundedUpdater : MonoBehaviour
{
    public PlayerAbstract Abstract;
    // Update is called once per frame
    public void GroundedUpdate()
    {
        if (Abstract.IsGrounded())
        {
            Debug.Log("Test1234");
        }
    }
}
