using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
    }
}
