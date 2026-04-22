using UnityEngine;
/// <summary>
/// This handels enemies taking damage. They will automatically take damage from a player's attack hitbox.
/// Damage can also be called on an enemy via a function.
/// </summary>
public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject ParentObject;
    [SerializeField] private Rigidbody2D rb;
    [Tooltip("Default health values: Slime 3, Para-Slimes 1, Goblin 5, Gliberknocker 6")]
    public int health = 1;
    private void Update() 
    {
        if (health <= 0)
        {
            Destroy(ParentObject);
        }
    }
    public void DamageEnemy(int damage, float xLaunch, float yLaunch, float damageSourceX)
    {
        //Debug.Log("Damaged Enemy for " + damage + " damage.");
        rb.linearVelocity = new Vector2(xLaunch*(transform.position.x <= damageSourceX ? -1 : 1), yLaunch);
        health -= damage;
    }
}