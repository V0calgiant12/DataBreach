using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Data Settings:")]
    public int playerHealth = 5;
    public float mudSpeedMulti;
    public float mudJumpMulti;
    [Header("Player Data References:")]
    public GameObject MainCamera;
    public Animator ScreenCanvas;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public BoxCollider2D collider; 
    public PlayerSound audioSource;
    public Animator anim;
    public Material pixelationMat;
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    public int iFrames;
    public bool sprinting;
    public bool crouching;
    public bool doubleJumpAvailable;
    public bool movementAllowed = true;
    public bool leftOrRight;
    public bool ricochet;
    public bool interacting;
    public bool inAirGust;
    public bool pickUpHeart;
    public bool playerDead;
    public bool inMud;
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