using UnityEngine;

public class GoblinPatrolling : GoblinAbstract
{
    public float patrolTargetX;
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        // New Patrol Target
        patrolTargetX = goblin.originPos.x + Random.Range(-goblin.patrolRange, goblin.patrolRange);
        goblin.anim.SetBool("moving", true);
        goblin.anim.SetBool("attacking", false);
        goblin.anim.SetBool("sprinting", false);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        float direction = patrolTargetX > goblin.transform.position.x ? 1 : -1;

        goblin.spriteHolder.transform.localScale = new Vector3(direction,1,1);

        // Walk forward (direction dependant) 
        goblin.goblinRb.linearVelocity = new Vector2(direction * goblin.moveSpeed/2 * goblin.mudSpeedMulti, goblin.goblinRb.linearVelocity.y);


        if (Mathf.Abs(goblin.transform.position.x - patrolTargetX) < 0.1f)
        {
            // New Patrol Target
            patrolTargetX = goblin.originPos.x + Mathf.Floor(Random.Range(-goblin.patrolRange, goblin.patrolRange)) + 0.5f;
            goblin.SwitchState(goblin.IdleState);
        }
        if (goblin.aggro)
        {
            goblin.Aggro();
        }
    }
}