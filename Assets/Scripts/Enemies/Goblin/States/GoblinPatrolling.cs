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
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        float direction = patrolTargetX > goblin.transform.position.x ? 1 : -1;
        
        // Walk forward (direction dependant) 
        goblin.goblinRb.linearVelocity = new Vector2(direction * goblin.moveSpeed/2, goblin.goblinRb.linearVelocity.y);


        if (Mathf.Abs(goblin.transform.position.x - patrolTargetX) < 0.5f)
        {
            // New Patrol Target
            patrolTargetX = goblin.originPos.x + Random.Range(-goblin.patrolRange, goblin.patrolRange);
        }
    }
}