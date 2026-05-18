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
    public bool isJumping;
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
        playerData.playerDead = false;
        playerData.playerHealth = 5;
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

        playerData.ScreenCanvas = GameObject.Find("Screen").GetComponent<Animator>();

    }
    void Update()
    {
        if (playerData.interacting && currentState != InteractingState)
        {
            SwitchState(InteractingState);
        }

        if (playerData.movementAllowed)
        {
            currentState.UpdateState(this); // Update function for current active state.
        }
        if (!playerData.playerDead)
        {
            GlobalUpdateState.UpdateState(this); // Update function for the Update state.
        }

        FindPlayerObject();
        playerSprite.transform.localScale = new Vector3(playerData.leftOrRight ? 1:-1,1,1);
        // Counter countdowns
        playerData.jumpBufferCounter -= Time.timeScale == 1 ? 1 : 0;
        playerData.coyoteTimeCounter -= Time.timeScale == 1 ? 1 : 0;
        playerData.iFrames -= Time.timeScale == 1 ? 1 : 0;
        playerData.anim.SetInteger("iframes", playerData.iFrames);

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
            //PlayerFlash(1);
            playerData.anim.SetBool("hit", true);
            TriggerShake.Instance.BurstShake(3,2);
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
            PlayerFlash(1);
            PlayerFlash(2);
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
                    playerData.anim.SetInteger("attackId",0);
                    break;
                case(AttackType.up):
                    playerData.anim.SetInteger("attackId",1);
                    break;
                case(AttackType.down):
                    playerData.anim.SetInteger("attackId",3);
                    break;
                case(AttackType.forwardAir):
                    playerData.anim.SetInteger("attackId",0);
                    break;
                case(AttackType.backAir):
                    playerData.anim.SetInteger("attackId",2);
                    break;
                case(AttackType.upAir):
                    playerData.anim.SetInteger("attackId",1);
                    break;
                case(AttackType.downAir):
                    if(playerData.PlayerRb.linearVelocityY < -5)
                    {
                        playerData.PlayerRb.linearVelocity = new Vector2(playerData.PlayerRb.linearVelocityX, -5f);
                    }
                    playerData.anim.SetInteger("attackId",4);
                    break;
                case(AttackType.dash):
                    playerData.movementAllowed = false;
                    playerData.anim.SetInteger("attackId",5);
                    StartCoroutine(NoMovingWhileAttack(0));
                    break;
            }
            Debug.Log(attackType);
        }
    }

    public void PlayerFlash(int type)
    {
        GameObject[] playerSprites = GameObject.FindGameObjectsWithTag("PlayerSprite"); // Puts all player sprite objects in a list.
        int index = 0;
        if(type == 1) // White Flash
        {
            Debug.Log("White Flash");
            while (index <= playerSprites.Length - 1) // Repeats for every game object.
            {
                playerSprites[index].SendMessage("WhiteFlash");
                index += 1;
            }
        }
        else if(type == 2) // Invulnerable Flash
        {
            Debug.Log("Invulnerable Flash");
            while (index <= playerSprites.Length - 1) // Repeats for every game object.
            {
                playerSprites[index].SendMessage("InvulnerableFlash", playerData.iFrames);
                index += 1;
            }
        }
    }
    public IEnumerator StunPlayer(float xLaunch, float yLaunch, int timer)
    {
        playerData.movementAllowed = false;
        int elapsed = 0;
        playerData.PlayerRb.linearVelocity = new Vector2(xLaunch, yLaunch);
        while(GroundCheck.Instance._IsGrounded == false && timer > elapsed || elapsed < 15)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
            if(playerData.ricochet == true)
            {
                playerData.PlayerRb.linearVelocity = new Vector2(-playerData.PlayerRb.linearVelocity.x + ((playerData.PlayerRb.linearVelocity.x >= 0 ? -1.2f : 1.2f) * xLaunch), playerData.PlayerRb.linearVelocity.y + yLaunch * 0.25f);
                playerData.ricochet = false;
                TriggerShake.Instance.BurstShake(-1*MathF.Cos(playerData.PlayerRb.linearVelocityX/2)+(2+elapsed/25),2);
                PlayerFlash(1);
                timer += 15;
            }
            if(playerData.pickUpHeart)
            {
                playerData.PlayerRb.linearVelocity = new Vector2(0, 0);
                playerData.pickUpHeart = false;
            }
            yield return null;
        }
        playerData.anim.SetBool("hit", false);
        playerData.movementAllowed = true;
    }
    public IEnumerator NoMovingWhileAttack(float attackTimer)
    {
        int elapsed = 0;
        playerData.anim.SetBool("moving", false);
        if(attackTimer == 0)
        {
            playerData.PlayerRb.linearVelocityX = 50 * ((playerData.PlayerRb.linearVelocityX > 0) ? 1 : -1);
            playerData.iFrames = Mathf.Abs(Mathf.FloorToInt(playerData.PlayerRb.linearVelocityX/0.8f))-30; // t=d/r, t=velocity/0.8f since velocity is multiplied by 0.8f every frame
            while (MathF.Abs(playerData.PlayerRb.linearVelocityX) > 1f)
            {
                playerData.PlayerRb.linearVelocityX = playerData.PlayerRb.linearVelocityX * 0.8f;
                yield return null;
            }
        }
        else
        {
            
            while (attackTimer > elapsed)
            {
                playerData.PlayerRb.linearVelocityX = playerData.PlayerRb.linearVelocityX * 0.75f;
                elapsed += Time.timeScale == 1 ? 1 : 0;
                yield return null;
            }
        }
        currentState = IdleState;
        playerData.anim.SetBool("attacking", false);
        playerData.movementAllowed = true;
    }
    public IEnumerator WaitUntilNotJumping()
    {
        isJumping = true;
        yield return new WaitUntil(() => playerData.PlayerRb.linearVelocityY < 0);
        isJumping = false;
    }
}