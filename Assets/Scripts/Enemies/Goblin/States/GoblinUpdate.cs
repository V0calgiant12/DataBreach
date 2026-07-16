using UnityEngine;

public class GoblinUpdate : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {

    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        if (goblin.attackRange.withinRange && goblin.currentAtkCd == 0)
        {
            goblin.SwitchState(goblin.AttackState);
        }
        if (goblin.enemyHit.damageTaken)
        {
            goblin.SwitchState(goblin.HurtState);
        }
    }
}