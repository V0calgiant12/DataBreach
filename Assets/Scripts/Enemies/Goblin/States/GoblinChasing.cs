using UnityEngine;

public class GoblinChasing : GoblinAbstract
{
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        goblin.wallTrigger.size = new Vector2(2f, goblin.wallTrigger.size.y);
        goblin.anim.SetBool("moving", true);
        goblin.anim.SetBool("attacking", false);
        goblin.anim.SetBool("sprinting", true);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        // Checks if the player is left (-1) or right (1) of the goblin
        float direction = PlayerStateManager.Instance.transform.position.x > goblin.transform.position.x ? 1 : -1;
        
        goblin.spriteHolder.transform.localScale = new Vector3(direction,1,1);

        // Walk forward (direction dependant)
        if(Mathf.Abs(PlayerStateManager.Instance.transform.position.x - goblin.transform.position.x) > 0.5)
        {
            goblin.goblinRb.linearVelocity = new Vector2(direction * goblin.moveSpeed * goblin.mudSpeedMulti, goblin.goblinRb.linearVelocity.y);
        }
        if (!goblin.aggro)
        {
            goblin.Deaggro();
        }
    }
}