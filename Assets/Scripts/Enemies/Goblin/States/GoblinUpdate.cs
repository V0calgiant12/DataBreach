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
        goblin.anim.SetBool("particles", SettingsData.Instance._Particles <= 1);
        goblin.anim.SetBool("hit", false);
        if (goblin.attackRange.withinRange && goblin.currentAtkCd <= 0 && goblin.groundCheck._IsGrounded)
        {
            goblin.SwitchState(goblin.AttackState);
        }
        if(goblin.currentState != goblin.AttackState)
        {
            goblin.currentAtkCd -= Time.deltaTime;
        }
    }
}