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
        if (goblin.attackRange.withinRange && goblin.currentAtkCd <= 0)
        {
            goblin.SwitchState(goblin.AttackState);
        }
        if (goblin.enemyHit._DamageTaken)
        {
            goblin.SwitchState(goblin.HurtState);
        }
        if(goblin.currentState != goblin.AttackState)
        {
            goblin.currentAtkCd -= Time.timeScale == 1 ? 1 : 0;
        }
        if(goblin.goblinRb.linearVelocityY < 0)
        {
            goblin.anim.SetBool("jumping", false);
            goblin.anim.SetBool("falling", true);
        }
    }
}