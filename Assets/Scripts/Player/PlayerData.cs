using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public int playerHealth = 5;
    public int fastFallCounter = 0;
    public int jumpBufferCounter = 0;
    public int coyoteTimeCounter = 0;
    public int interactingCooldown = 0;
    public int iFrames = 0;
    public int ricochet = 0;
    public float mudSpeedMulti = 1;
    public float mudJumpMulti = 1;
    public Vector2 lastCheckpoint = new Vector2(0,0);
    public int bufferedAtk = 0;
    public Vector2 bufferedAtkDir = new Vector2(0,0);
    [Header("Movement")]
    public bool sprinting = false;
    public bool crouching = false;
    public bool doubleJumpAvailable = true;
    public bool movementAllowed = true;
    [Header("Checks")]
    public bool leftOrRight = false;
    public bool interacting = false;
    public bool inAirGust = false;
    public bool pickUpHeart = false;
    public bool playerDead = false;
    public bool inKnockback = false;
    public bool inMud = false;
    [Header("References")]
    public GameObject MainCamera;
    public Animator ScreenCanvas;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public BoxCollider2D collider; 
    public PlayerSound audioSource;
    public Animator anim;
    public Material pixelationMat;
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
    public AudioClip _PlayerAttack;
    public AudioClip[] _MudWalk;
}