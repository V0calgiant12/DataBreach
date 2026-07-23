using UnityEngine;

public class GoblinDead : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.anim.SetBool("dead", true);
        goblin.goblinRb.linearVelocityX = 0;
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
    }
}