using UnityEngine;

public class EnemyWallTrigger : MonoBehaviour
{

    public static EnemyWallTrigger instance;
    public GameObject ParentObject;
    public bool wallCollision;

    void OnTriggerStay2D(Collider2D other)
    {
        wallCollision = true;
        if(ParentObject.gameObject.name == "Goblin")
        {
            ParentObject.GetComponent<GoblinStateManager>().WallCollision();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        wallCollision = false;
        if(ParentObject.gameObject.name == "Goblin")
        {
            ParentObject.GetComponent<GoblinStateManager>().touchingWall = false;
        }
    }
}
