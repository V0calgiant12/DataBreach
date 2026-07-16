using UnityEngine;

public class GoblinHurt : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.goblinRb.linearVelocity = goblin.enemyHit._LastKnockbackTaken;
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        //This state is supposed to be mostly empty.

        if (!goblin.enemyHit._DamageTaken && goblin.groundCheck._IsGrounded)
        {
            if(goblin.currentAtkCd != 0)
            {
                goblin.SwitchState(goblin.AttackState);
            }
            else
            {
                goblin.SwitchState(goblin.ChasingState);
            }
        }
    }
}