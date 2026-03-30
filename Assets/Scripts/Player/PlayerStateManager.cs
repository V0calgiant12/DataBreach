using System.Collections;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerUpdate GlobalUpdateState = new PlayerUpdate();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    public PlayerDead DeadState = new PlayerDead();
    public PlayerInteracting InteractingState = new PlayerInteracting();
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    public static PlayerStateManager Instance;
    public PlayerData playerData;
    public void Intereact()
    {
        playerData.interacting = true;
    }
    void Start()
    {
        playerData.playerHealth = 5;
        playerData.interacting = false;
        Instance = this;
        FindPlayerObject();
        currentState = IdleState;
        currentState.RunOnce(this);
        GlobalUpdateState.EnterState(this);
        currentState.EnterState(this);

    }
    void Update()
    {
        if (playerData.interacting && currentState != InteractingState)
        {
            SwitchState(InteractingState);
        }
        if (playerData.movementAllowed)
        {
            currentState.UpdateState(this);
        }
        GlobalUpdateState.UpdateState(this);
        FindPlayerObject();

        // Counter countdowns
        playerData.jumpBufferCounter -= 1;
        playerData.coyoteTimeCounter -= 1;

    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    public void FindPlayerObject()
    {
        playerData.anim = GetComponent<Animator>(); 
        playerData.PlayerRb = gameObject.GetComponent<Rigidbody2D>();
        playerData.audioSource = gameObject.GetComponent<PlayerSound>();
        playerData.collider = gameObject.GetComponent<BoxCollider2D>();
        playerData.MainCamera = GameObject.Find("Main Camera");
    }
    public void DamagePlayer(float xLaunch, float yLaunch,int timer)
    {
        if (playerData.iFrames == 0)
        {
            playerData.playerHealth = playerData.playerHealth - 1;
            Debug.Log(playerData.playerHealth);
            StartCoroutine(StunPlayer(xLaunch*(playerData.leftOrRight ? -1 : 1), yLaunch,timer));
            playerData.iFrames = 60;
        }
        while (playerData.iFrames > 0)
        {
            Debug.Log(playerData.iFrames);
            playerData.iFrames -= 1;
            if (playerData.iFrames < 0) 
            {
                playerData.iFrames = 0;
            }
        }
    }
    public void Attack()
    {
        playerData.movementAllowed = false;
        playerData.attackTimer = 60f;
        StartCoroutine(NoMovingWhileAttack(playerData.attackTimer));
    }
    public IEnumerator StunPlayer(float xLaunch, float yLaunch, int timer)
    {
        playerData.movementAllowed = false;
        int elapsed = 0;
        playerData.PlayerRb.linearVelocity = new Vector2(xLaunch, yLaunch);
        while(GroundCheck.Instance._IsGrounded == false && timer > elapsed)
        {
            elapsed += 1;
            yield return null;
        }
        playerData.movementAllowed = true;
    }
    public IEnumerator NoMovingWhileAttack(float attackTimer)
    {
        int elapsed = 0;
        playerData.PlayerRb.linearVelocityX = 0;
        while (attackTimer > elapsed)
        {
            elapsed += 1;
            yield return null;
        }
        playerData.movementAllowed = true;
    }
}