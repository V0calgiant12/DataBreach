using UnityEngine;

public class GoblinDead : GoblinAbstract
{
    private int elapsed = 0;
    public override void RunOnce(GoblinStateManager goblin)
    {

    }
    public override void EnterState(GoblinStateManager goblin)
    {
        elapsed = 0;
        goblin.anim.SetBool("dead", true);
        goblin.goblinRb.linearVelocityX = 0;
        goblin.goblinRb.linearVelocityY = 0;
        goblin.audioSource.PlaySound(goblin.goblinDeath,1,1,1,1,goblin.transform.position);
    }
    public override void UpdateState(GoblinStateManager goblin)
    {
        elapsed += Time.timeScale == 1 ? 1 : 0;
        Debug.Log(elapsed);
        if (elapsed >= 225)
        {
            goblin.Kill();
        }
    }
}