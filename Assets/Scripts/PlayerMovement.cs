using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;

    private float direction = 0f;

    [SerializeField] LayerMask groundLayer;

    public float speed = 7f;
    public float jumpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() {
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    bool IsGrounded() {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
}
