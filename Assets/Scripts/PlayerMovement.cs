using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region dependencies
    Rigidbody2D rb;
    BoxCollider2D coll;

    private float direction = 0f;

    [SerializeField] LayerMask groundLayer;

    public float speed = 7f;
    private float jumpSpeed = 12f;
    private float extraJumpSpeed = 12f;
    private float coyoteJumpSpeed = 16f;
    public float maxJumpSpeed = 12f;
    [HideInInspector] public bool IsFacingRight = true;
    private bool IsJumping = false;
    private int ExtraJumps = 1;
    
    private bool CountLastOnGroundTime = true;

    private bool wasJumping = false;
    private bool jumpCut = false;
    private float LastPressedJumpTime;
	private float LastOnGroundTime;

    private float coyoteTime;
    private bool canCountCoyoteTime = true;

    private GameObject playerGFX;

    public GameObject deity;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        jumpSpeed = maxJumpSpeed;
        playerGFX = this.gameObject.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timers();
        JumpChecks();
        Movement();
    }

    #region functions

    void Movement() {
        //Moving left and right
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        //Flips player sprite depending on if the player is facing right or not
        if(rb.velocity.x > 0) {
            IsFacingRight = true;
        }
        if(rb.velocity.x < 0) {
            IsFacingRight = false;
        }

        if(IsFacingRight) {
            playerGFX.transform.localScale = new Vector3(1, 1, 1);
        } else if(!IsFacingRight) {
            playerGFX.transform.localScale = new Vector3(-1, 1, 1);
        }

        #region Jump
        //Call Jump
        if(Input.GetButtonDown("Jump") && IsGrounded() && CanJump() && LastPressedJumpTime < 0) {
            IsJumping = true;
            Jump(false, false);
        }

        // Coyote Jump
        if(Input.GetButtonDown("Jump") && !IsGrounded() && coyoteTime < 0 && coyoteTime > -0.06f) {
            coyoteTime = 0;
            canCountCoyoteTime = false;
            IsJumping = true;
            Jump(true, false);
            print("coyote jump!");
        }

        // Extra Jump
        if(Input.GetButtonDown("Jump") && !IsGrounded() && ExtraJumps > 0 && coyoteTime != 0) {
            IsJumping = true;
            Jump(false, true);
        }

        //Jump Cut
        if(Input.GetButtonUp("Jump") && !IsGrounded() && !jumpCut) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale *= 1.5f;
            jumpCut = true;
        } 

        //Reset jumpSpeed
        if(!wasJumping) {
            jumpSpeed = maxJumpSpeed;
        }

        //Reset WasJumping;
        if(CanJump()) {
            wasJumping = false;
        }

        if(IsGrounded()) {
            canCountCoyoteTime = false;
            coyoteTime = -0.01f;
            ExtraJumps = 1;
        } else if(!IsGrounded()) {
            canCountCoyoteTime = true;
        }

        //Reset jumpcut
        if(jumpCut && IsGrounded()) {
            rb.gravityScale /= 1.5f;
            jumpCut = false;
        }

        #endregion
    }

    //Jump
    public void Jump(bool coyoteJump, bool ExtraJump) {
        LastPressedJumpTime = 0.5f;
        if(rb.velocity.y < 0) {
            jumpSpeed = 0;
        }
        if(coyoteJump) {
            rb.AddForce(Vector2.up * coyoteJumpSpeed, ForceMode2D.Impulse);
        } else if(ExtraJump) {
            ExtraJumps = 0;
            rb.velocity = new Vector2(rb.velocity.x, extraJumpSpeed);
        } else {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        
    }

    //Check Jump details
    void JumpChecks() {
        if(IsJumping && rb.velocity.y <= 0) {
            IsJumping = false;
            wasJumping = true;
        }
    }

    //Check if player is on ground
    bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    //Dont check if IsGrouned if player is already on ground (Optimization feature)
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "PlayerGround") {
            CountLastOnGroundTime = false;
            LastOnGroundTime = 1;
        }
    }

    //Start counting Airtime (Time player is not grounded)
    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "PlayerGround") {
            CountLastOnGroundTime = true;
        }
    }

    //Timers for grounded and jumptime
    void Timers() {
        LastPressedJumpTime -= Time.deltaTime;
        if(CountLastOnGroundTime) {
            LastOnGroundTime -= Time.deltaTime;
        }
        if(canCountCoyoteTime) {
            coyoteTime -= Time.deltaTime;
        }
    }

    //Check if player can jump
    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

    #endregion
}