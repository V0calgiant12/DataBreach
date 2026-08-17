using UnityEngine;

public class PlayerDead : PlayerAbstract
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int pixelation = 550;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player)
    {
        pixelation = 550;
        player.playerData.pixelationMat.SetFloat("_Pixelation", 550);
        player.playerData.movementAllowed = false;
        player.playerData.playerDead = true;
        player.playerData.inKnockback = false;
        player.playerData.anim.SetBool("dead", true);
        player.playerData.anim.SetBool("moving", false);
        player.playerData.anim.SetBool("sprinting", false);
        player.playerData.anim.SetBool("jumping", false);
        player.playerData.anim.SetBool("falling", false);
        player.playerData.anim.SetBool("crouching", false);
        player.playerData.PlayerRb.linearVelocityX = 0;
        player.playerData.audioSource.PlayPlayerDeathSound(player.playerData._PlayerDeath);
        player.playerData.ScreenCanvas.SetTrigger("Death");
        Debug.Log("You Are Dead");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if(player.playerData.OffsetVelocity != new Vector2(0, 0))
        {
            player.playerData.PlayerRb.linearVelocity = player.playerData.OffsetVelocity;
        }
        if(pixelation > 10)
        {
            pixelation -= 5;
            player.playerData.pixelationMat.SetFloat("_Pixelation", pixelation);
            
        }
        else
        {
            player.playerData.pixelationMat.SetFloat("_Pixelation", 550);
        }
        //Debug.Log(player.playerData.pixelationMat.GetFloat("_Pixelation") + " " + pixelation);
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
}