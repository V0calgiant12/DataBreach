using UnityEngine;

public abstract class GoblinAbstract
{
    // States
    public abstract void RunOnce(GoblinStateManager goblin);
    public abstract void EnterState(GoblinStateManager goblin);
    public abstract void UpdateState(GoblinStateManager goblin);
}