using UnityEngine;

public class GoblinHurt : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.goblinRb.linearVelocity = goblin.enemyHit._LastKnockbackTaken;
        goblin.anim.SetBool("hit", true);
        goblin.anim.SetBool("attacking", false);
        Debug.Log("hurtstate");
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        //This state is supposed to be mostly empty.

        if (!goblin.enemyHit._DamageTaken && goblin.groundCheck._IsGrounded)
        {
            goblin.anim.SetBool("hit", false);
            if(goblin.currentAtkCd <= 10)
            {
                goblin.currentAtkCd += 10;
            }
            goblin.SwitchState(goblin.ChasingState);
        }
    }
}