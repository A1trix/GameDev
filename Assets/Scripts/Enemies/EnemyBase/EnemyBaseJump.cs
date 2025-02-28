using UnityEngine;

public class EnemyBaseJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float jumpCooldown = 2f;
    public bool canJump = true;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundCheck == null)
        {
            Debug.LogWarning("GroundCheck is not set. Assign a child Transform to groundCheck.");
        }
    }

    // Checks if the enemy is on the ground using a small overlap circle.
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Enemy jump with jumpForce
    public void PerformJump()
    {
        if (isGrounded())
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity before applying the jump force
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        OnJump();                                   // Trigger any additional jump behavior
    }

    // Additional logic to be executed when the enemy jumps
    // Can be overridden in derived classes for specific enemy behavior
    protected virtual void OnJump()
    {
        Debug.Log($"{gameObject.name} has jumped!");
    }

    // Visualization
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
