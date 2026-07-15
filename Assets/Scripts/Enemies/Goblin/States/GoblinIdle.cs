using UnityEngine;

public class GoblinIdle : GoblinAbstract
{
    private int idleTime;
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        idleTime = 30;
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        idleTime -= 1;
        goblin.goblinRb.linearVelocityX = 0;

        if(idleTime == 0){
            goblin.SwitchState(goblin.PatrollingState);
        }
    }
}