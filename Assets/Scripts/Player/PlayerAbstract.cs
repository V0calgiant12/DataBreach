using UnityEngine;

public abstract class PlayerAbstract
{
    // States
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    //public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
    public abstract void RunOnce(PlayerStateManager player);

    public Vector2 PlayerVelocity;
    public Vector2 OffsetVelocity;
    public float playerSpeed = 12f;
    public float jumpStrength = 20f;
    public int lastWallJumpRight;
    public bool fakeCrouchToggle;
    public bool moving;
    public bool currentWallSide;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public void Setup() 
    {
        //fakeCrouchToggle = true;
    }
    public void Update()
    {
        
        
    }
}