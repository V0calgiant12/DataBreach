using UnityEngine;
using System.Collections;

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
    public float jumpStrength = 18.5f;
    public float shakeIntensityLvl;
    public int lastWallJumpRight;
    public bool fakeCrouchToggle;
    public bool moving;
    public bool currentWallSide;
    public bool shakeOnLand;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    
    public void Setup() 
    {
        //fakeCrouchToggle = true;
    }
    public void Update()
    {
        
        
    }
}