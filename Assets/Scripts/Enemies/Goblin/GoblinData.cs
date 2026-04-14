using UnityEngine;

[CreateAssetMenu(fileName = "GoblinData", menuName = "ScriptableObjects/GoblinData")]
public class GoblinData : ScriptableObject
{
    [Header("Movement & Gravity")]
    public float moveSpeed = 3f;
    public float patrolRange = 5f;
    public float chaseRange = 7f;

    [Header("Combat")]
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;


    [Header("References")]
    public Transform player;

    private Rigidbody2D goblinRb;
    private Vector2 startPosition;
    private float patrolTargetX;
    private SpriteRenderer spriteRenderer;
}
