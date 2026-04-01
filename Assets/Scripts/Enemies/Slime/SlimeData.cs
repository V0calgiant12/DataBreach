using UnityEngine;

[CreateAssetMenu(fileName = "SlimeData", menuName = "ScriptableObjects/SlimeData")]
public class SlimeData : ScriptableObject
{
    public enum State { Idle, Chase }
    public State currentState = State.Idle;

    public float jumpForce = 5f;      
    public float forwardForce = 3f;  
    public float timeBetweenJumps = 1.5f;

    public float detectionRange = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D slimeRb;
    private Transform player;
    private float jumpTimer;
    public int slimeHealth;
    private bool isGrounded;
}