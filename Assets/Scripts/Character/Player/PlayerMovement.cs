using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float airControlMultiplier = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;

        if (IsGrounded())
        {
            velocity.x = horizontalInput * moveSpeed;
        }
        else
        {
            velocity.x = horizontalInput * moveSpeed * airControlMultiplier;
        }

        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }

    // removing grounded as a separate method and calling it when needed?
    private bool IsGrounded()
    {
        isGrounded = GetComponent<PlayerJump>().IsGrounded();
        return isGrounded;
    }
}
