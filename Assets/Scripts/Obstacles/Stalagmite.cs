using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public BoxCollider2D[] boxColliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StalagmiteRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StalagmiteRb.gravityScale = 1f;
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Test");
            Collider.SetActive(true);
        }
    }
    
}