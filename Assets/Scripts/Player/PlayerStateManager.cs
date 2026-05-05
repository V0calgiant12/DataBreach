using System;
using System.Collections;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Player State Manager References:")]
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerUpdate GlobalUpdateState = new PlayerUpdate();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    public PlayerDead DeadState = new PlayerDead();
    public PlayerInteracting InteractingState = new PlayerInteracting();
    public static PlayerStateManager Instance;
    public PlayerData playerData;
    public GameObject playerSprite;
    public enum AttackType
    {
        forward,
        up,
        down,
        dash,
        forwardAir,
        backAir,
        downAir,
        upAir
    }
    public void Interact()
    {
        playerData.interacting = true;
    }
    void Start()
    {
        playerData.sprinting = false;
        playerData.leftOrRight = true;
        playerData.crouching = false;
        playerData.movementAllowed = true;
        playerData.OffsetVelocity = new Vector2(0,0);
        playerData.interacting = false;
        Instance = this;
        FindPlayerObject();
        currentState = IdleState;
        currentState.RunOnce(this);
        GlobalUpdateState.EnterState(this);
        currentState.EnterState(this);
        
        playerData.anim.SetBool("attacking", false);
        playerData.anim.SetBool("moving", false);
        playerData.anim.SetBool("sprinting", false);

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
        playerSprite.transform.localScale = new Vector3(playerData.leftOrRight ? 1:-1,1,1);
        // Counter countdowns
        playerData.jumpBufferCounter -= 1;
        playerData.coyoteTimeCounter -= 1;
        playerData.iFrames -= 1;

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
    public void DamagePlayer(float xLaunch, float yLaunch,int timer, bool overrideIFrames, float damageSourceX, bool nonDirectional)
    {
        if (playerData.iFrames < 0 || overrideIFrames)
        {
            playerData.playerHealth = playerData.playerHealth - 1;
            playerData.audioSource.PlayPlayerHitSound(playerData._PlayerHit);
            //Debug.Log(playerData.playerHealth);
            if (nonDirectional)
            {
                StartCoroutine(StunPlayer(xLaunch*(playerData.leftOrRight ? -1 : 1), yLaunch,timer));
            }
            else
            {
                StartCoroutine(StunPlayer(xLaunch*(transform.position.x <= damageSourceX ? -1 : 1), yLaunch,timer));
            }
            playerData.iFrames = 120;
        }
    }
    public void Attack(AttackType attackType)
    {
        if(playerData.anim.GetBool("attacking") != true)
        {
            playerData.anim.SetBool("attacking", true);
            playerData.audioSource.PlayPlayerAttackSound(playerData._PlayerAttack);
            switch (attackType)
            {
                case(AttackType.forward):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",0);
                    break;
                case(AttackType.up):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",1);
                    break;
                case(AttackType.down):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",3);
                    break;
                case(AttackType.forwardAir):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",0);
                    break;
                case(AttackType.backAir):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",2);
                    break;
                case(AttackType.upAir):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",1);
                    break;
                case(AttackType.downAir):
                    playerData.attackTimer = 0;
                    playerData.anim.SetInteger("attackId",4);
                    break;
                case(AttackType.dash):
                    playerData.attackTimer = 1;
                    playerData.movementAllowed = false;
                    playerData.anim.SetInteger("attackId",5);
                    StartCoroutine(NoMovingWhileAttack(playerData.attackTimer));
                    break;
            }
            Debug.Log(attackType);
        }
    }
    public IEnumerator StunPlayer(float xLaunch, float yLaunch, int timer)
    {
        playerData.movementAllowed = false;
        int elapsed = 0;
        playerData.PlayerRb.linearVelocity = new Vector2(xLaunch, yLaunch);
        while(GroundCheck.Instance._IsGrounded == false && timer > elapsed || elapsed < 15)
        {
            elapsed += 1;
            if(playerData.ricochet == true)
            {
                playerData.PlayerRb.linearVelocity = new Vector2(-playerData.PlayerRb.linearVelocity.x + ((playerData.PlayerRb.linearVelocity.x >= 0 ? -1.2f : 1.2f) * xLaunch), playerData.PlayerRb.linearVelocity.y + yLaunch * 0.25f);
                playerData.ricochet = false;
                TriggerShake.Instance.BurstShake(elapsed/2,2);
                timer += 60;
            }
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