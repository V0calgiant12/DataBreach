using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    public abstract void OnGroundTouch();
    public abstract void OnGroundLeave();
    public abstract void OnHit();
}