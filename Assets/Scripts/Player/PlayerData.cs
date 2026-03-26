using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject MainCamera;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public BoxCollider2D collider; 
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    //public int lastWallJumpRight;
    public bool sprinting;
    public bool crouching;
    public bool doubleJumpAvailable;
    //public bool currentWallSide;
}