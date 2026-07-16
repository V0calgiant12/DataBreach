using UnityEngine;

public class GoblinHurt : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        //This state is supposed to be mostly empty.

        if (!goblin.enemyHit.damageTaken && goblin.groundCheck._IsGrounded)
        {
            goblin.SwitchState(goblin.ChasingState);
        }
    }
}