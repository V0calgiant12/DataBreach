using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Ground Check References:")]
    
    public bool _IsGrounded;
    public bool _IsStone;
    public bool _IsWood;
    public bool _InCutscene = false;
    public static GroundCheck Instance;
    private PlayerStateManager playerStateManager;

    void Start()
    {
        Instance = this;
        playerStateManager = transform.parent.GetComponent<PlayerStateManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_InCutscene && other.gameObject.CompareTag("Ground") && GeneralCutsceneManager.Instance.cutscene == GeneralCutsceneManager.CutsceneType.Intro)
        {
            TriggerShake.Instance.BurstShake(15,1,true,0);
            UpdateVariables(other);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        UpdateVariables(other);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone") || other.gameObject.CompareTag("Wood"))
        {
            _IsGrounded = false;
        }
        if (!_InCutscene)
        {
            if (other.gameObject.CompareTag("Stone") && playerStateManager.playerData.coyoteTimeCounter < 0)
            {
                _IsStone = false;
            }
        }
    }
    private void UpdateVariables(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = true;
        }
        else
        {
            _IsStone = false;
        }
        if (other.gameObject.CompareTag("Wood"))
        {
            _IsWood = true;
        }
        else
        {
            _IsWood = false;
        }
        if (!_InCutscene)
        {
            playerStateManager.UpdateSounds();
        }
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone")||other.gameObject.CompareTag("Wood"))
        {
            _IsGrounded = true;
        }
    }
}