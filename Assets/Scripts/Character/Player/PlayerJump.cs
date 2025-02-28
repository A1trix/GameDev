using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float groundCheckRadius;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public PlayerMovement player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleJumping()
    {
        float verticalMove = player.joystick.Vertical;
        if (verticalMove >= 0.5f && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
