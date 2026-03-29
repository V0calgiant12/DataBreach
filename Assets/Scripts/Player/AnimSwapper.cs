using UnityEngine;

public class AnimSwapper : MonoBehaviour
{
    public GameObject AnimHolder;
    public GameObject AirAnimHolder;
    public Animator anim;
    public Animator animAir;
    // Update is called once per frame
    void Start()
    {
        anim = gameObject.AddComponent<Animator>();
    }
    void Update()
    {
        if (!GroundCheck.Instance._IsGrounded)
        {
            anim = AirAnimHolder.GetComponent<Animator>();
        }
        else
        {
            anim = AnimHolder.GetComponent<Animator>(); 
        }
    }
}
