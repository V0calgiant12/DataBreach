using UnityEngine;

public class GoblinAttack : GoblinAbstract
{
    int elapsed = 0;
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.anim.SetBool("attacking", true);
        // Attack animation begin
        elapsed = 0;
        goblin.audioSource.PlaySound(goblin.goblinReady,1,1,1);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        goblin.spriteHolder.transform.localScale = new Vector3(PlayerStateManager.Instance.transform.position.x > goblin.transform.position.x ? 1:-1,1,1);

        elapsed += Time.timeScale == 1 ? 1 : 0;

        if(elapsed == 25)
        {
            goblin.audioSource.PlaySound(goblin.goblinAttack,1,1,1);
        } 

        if(goblin.anim.GetBool("attacking") == false)
        {
            goblin.SwitchState(goblin.ChasingState);
            goblin.currentAtkCd = goblin.attackCD;
        }
        if(Mathf.Abs(goblin.goblinRb.linearVelocityX) > 0.1)
        {
            goblin.goblinRb.linearVelocity = new Vector2(goblin.goblinRb.linearVelocityX * 0.8f, goblin.goblinRb.linearVelocityY);
        }
        else
        {
            goblin.goblinRb.linearVelocity = new Vector2(0,goblin.goblinRb.linearVelocityY);
        }
    }
}