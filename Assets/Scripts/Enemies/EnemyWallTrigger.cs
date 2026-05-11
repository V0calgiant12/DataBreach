using UnityEngine;

public class EnemyWallTrigger : MonoBehaviour
{

    public static EnemyWallTrigger instance;
    public GameObject Goblin;
    public bool wallCollision;

    void Start()
    {
       // stateManager = GetComponent<GoblinStateManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        wallCollision = true;
        Debug.Log("wall collision");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        wallCollision = false;
    }
}
