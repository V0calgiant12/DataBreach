using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Sawblade SawbladeRef;
    public GameObject WallDetectLeft;
    public GameObject WallDetectRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SawbladeRef.sawbladeDirection)
        {
            WallDetectLeft.SetActive(false);
        }
        if (!SawbladeRef.sawbladeDirection)
        {
            WallDetectRight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Saw test");
            // Start to go back underground
        }
    }
}
