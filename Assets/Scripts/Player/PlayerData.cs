using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject MainCamera;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public BoxCollider2D collider; 
    public PlayerSound audioSource;
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    public int playerHealth = 5;
    public int iFrames;
    //public int oneOrTwo;
    //public int lastWallJumpRight;
    public bool sprinting;
    public bool crouching;
    public bool doubleJumpAvailable;
    public bool movementAllowed = true;
    public bool leftOrRight;
    public float attackTimer = 0f;
    public Animator anim;
    //public bool currentWallSide;
    public bool interacting;
    [Header("Audio")]
    public AudioClip _GrassWalk;
    public AudioClip _GrassFall;
    public AudioClip _GrassJump;
    public AudioClip _StoneWalk;
    public AudioClip _StoneFall;
    public AudioClip _StoneJump;
    public AudioClip _NormalFall;
    public AudioClip _NormalJump;
    public AudioClip _AirJump;
    public AudioClip _PlayerHit;
    public AudioClip _PlayerDeath;
    //public AudioClip _PlayerDeath2;
    public AudioClip _PlayerAttack;
}