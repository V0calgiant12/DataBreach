using UnityEngine;

public class GoblinChasing : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.wallTrigger.size = new Vector2(2f, goblin.wallTrigger.size.y);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        // Checks if the player is left (-1) or right (1) of the goblin
        float direction = PlayerStateManager.Instance.transform.position.x > goblin.transform.position.x ? 1 : -1;

        // Walk forward (direction dependant) 
        goblin.goblinRb.linearVelocity = new Vector2(direction * goblin.moveSpeed, goblin.goblinRb.linearVelocity.y);
    }
}