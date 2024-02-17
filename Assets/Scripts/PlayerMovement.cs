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
    public float jumpSpeed = 12f;
    public float maxJumpSpeed = 12f;
    public bool IsFacingRight = true;
    [SerializeField] private bool IsJumping = false;
    
    private bool CountLastOnGroundTime = true;

    private bool wasJumping = false;
    private bool jumpCut = false;
    [SerializeField] private float LastPressedJumpTime;
	[SerializeField] private float LastOnGroundTime;

    private GameObject playerGFX;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
            playerGFX.transform.localScale = new Vector3(1, 1, 1);
        }
        if(rb.velocity.x < 0) {
            IsFacingRight = false;
            playerGFX.transform.localScale = new Vector3(-1, 1, 1);
        }

        #region Jump
        //Call Jump
        if(Input.GetButtonDown("Jump") && IsGrounded() && CanJump() && LastPressedJumpTime < 0) {
            IsJumping = true;
            Jump();
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

        //Reset jumpcut
        if(jumpCut && IsGrounded()) {
            rb.gravityScale /= 1.5f;
            jumpCut = false;
        }

        #endregion
    }

    //Jump
    void Jump() {
        LastPressedJumpTime = 0.5f;
        if(rb.velocity.y < 0) {
            jumpSpeed = 0;
        }
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
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
    }

    //Check if player can jump
    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

    #endregion
}