using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float airControlMultiplier = 0.5f;
    public float jumpForce = 10f;

    public Animator animator;

    // public float wallSlideSpeed = 2f;
    // public float wallJumpForce = 10f;

    [Header("Physics Settings")]
    public LayerMask groundLayer;

    public float groundCheckRadius;
    public Transform groundCheck;
    // public Transform wallCheck;
    // public float wallCheckDistance = 0.5f;

    [Header("Jump Settings")]
    private Rigidbody2D rb;
    // private bool isOnWall;
    // private bool isWallSliding;
    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJumping();
        // HandleWallSliding();
        FlipCharacter();
    }

    private void FixedUpdate()
    {
        isGrounded();
        // CheckWall();

        if (isGrounded())
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;
        animator.SetFloat("moveSpeed", Mathf.Abs(velocity.x));

        if (isGrounded())
        {
            velocity.x = horizontalInput * moveSpeed;
        }
        else
        {
            velocity.x = horizontalInput * moveSpeed * airControlMultiplier;
        }

        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }

    private void HandleJumping()
    {
        // if (Input.GetButtonDown("Jump") && (isGrounded || isWallSliding))
        if (Input.GetButtonDown("Jump") && isGrounded() )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            // if (isWallSliding)
            // {
            //     rb.AddForce(new Vector2(-wallCheckDistance * (facingRight ? 1 : -1), wallJumpForce), ForceMode2D.Impulse);
            //     FlipCharacter();
            // }
        }
    }

    // private void HandleWallSliding()
    // {
    //     if (isOnWall && !isGrounded && Input.GetAxis("Horizontal") != 0)
    //     {
    //         isWallSliding = true;
    //         rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    //     }
    //     else
    //     {
    //         isWallSliding = false;
    //     }
    // }

    private void FlipCharacter()
    {
        if (Input.GetAxis("Horizontal") > 0 && !facingRight || Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
    //     {
    //         isGrounded = false;
    //     }
    // }

    private bool isGrounded()
    {
        // Check if there's any collider on the groundLayer within the circle at groundCheck position
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        if (grounded)
        {
            Debug.Log("Grounded");
        }
        else 
        {
            Debug.Log("Not Grounded");
        }
        
        return grounded;
    }

    // private void CheckWall()
    // {
    //     Vector2 wallCheckDirection = facingRight ? Vector2.right : Vector2.left;
    //     isOnWall = Physics2D.Raycast(wallCheck.position, wallCheckDirection, wallCheckDistance, groundLayer);
    // }

    private void OnDrawGizmosSelected()
    {
        //     if (groundCheck != null)
        // {
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        // }

        // if (wallCheck != null)
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * (facingRight ? wallCheckDistance : -wallCheckDistance));
        // }
    }

    private void OnDrawGizmos()
    {
       Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Adjust the position to match the center of the box cast
    }


}
