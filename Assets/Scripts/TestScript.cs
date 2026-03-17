using UnityEngine;

public class TestScript : MonoBehaviour
{
    //https://prod.liveshare.vsengsaas.visualstudio.com/join?252C93F77147877C38E5BF4A64F80B535632
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.05f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.05f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(transform.position.x + 0.05f, transform.position.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
