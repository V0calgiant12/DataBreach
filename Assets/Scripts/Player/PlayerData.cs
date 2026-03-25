using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public BoxCollider2D collider; 
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    //public int lastWallJumpRight;
    public bool fakeSprintToggle;
    public bool sprinting;
    public bool fakeCrouchToggle;
    public bool doubleJumpAvailable;
    //public bool currentWallSide;
}