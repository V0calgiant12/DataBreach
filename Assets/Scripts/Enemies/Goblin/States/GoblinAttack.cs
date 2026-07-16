using UnityEngine;

public class GoblinAttack : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.currentAtkCd = goblin.attackCD;
        // Attack animation begin
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        goblin.currentAtkCd -= Time.timeScale == 1 ? 1 : 0;
        if(goblin.currentAtkCd == 0)
        {
            goblin.SwitchState(goblin.ChasingState);
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