using UnityEngine;

public class GoblinIdle : GoblinAbstract
{
    private int idleTime;
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        idleTime = Random.Range(15,45);
        goblin.wallTrigger.size = new Vector2(1.5f, goblin.wallTrigger.size.y);
        goblin.anim.SetBool("moving", false);
        goblin.anim.SetBool("attacking", false);
        goblin.anim.SetBool("sprinting", false);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        idleTime -=  Time.timeScale == 1 ? 1 : 0;;
        goblin.goblinRb.linearVelocityX = 0;

        if(idleTime == 0){
            goblin.SwitchState(goblin.PatrollingState);
        }
    }
}