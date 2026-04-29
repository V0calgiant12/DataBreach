using UnityEngine;

public class SideCheck : MonoBehaviour
{
    [Header("Ground Check References:")]
    
    [SerializeField] private PlayerData playerData;
    public bool _IsStone;
    public static SideCheck Instance;
    void Start()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Ricochet" + other);
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            playerData.ricochet = true;
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
}