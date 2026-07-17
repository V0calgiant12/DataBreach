using UnityEngine;

public class SideCheck : MonoBehaviour
{
    [Header("Ground Check References:")]
    
    [SerializeField] private PlayerData playerData;
    public bool _IsStone;
    public static SideCheck Instance;
    public GameObject collided;
    void Start()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        collided = other.gameObject;
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            playerData.ricochet = gameObject.name== "LeftSide" ? 1:2;
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = true;
        }
        else
        {
            _IsStone = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        collided = other.gameObject;
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            playerData.ricochet = 0;
        }
    }
}