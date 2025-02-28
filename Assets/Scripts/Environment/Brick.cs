using UnityEngine;

public class Brick : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float fallSpeed = 5f; // Speed at which the brick falls
    [SerializeField] private float riseSpeed = 3f; // Speed at which the brick rises
    [SerializeField] private Transform groundCheck; // Position to check for ground collision
    [SerializeField] private Vector2 groundCheckBox; // Size of the box used to check if the brick has hit the ground
    [SerializeField] private LayerMask groundLayer; // Layer mask for the ground

    private bool isFalling = true; // Tracks if the brick is falling or rising
    private Vector2 originalPosition; // Stores the brick's initial position

    private void Start()
    {
        // Store the brick's initial position
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isFalling)
        {
            // Move the brick downward
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

            // Check if the brick has hit the ground
            if (IsGrounded())
            {
                isFalling = false; // Switch to rising mode
            }
        }
        else
        {
            // Move the brick upward
            transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);

            // Check if the brick has returned to its original position
            if (transform.position.y >= originalPosition.y)
            {
                // Reset to falling mode
                isFalling = true;
            }
        }
    }

    private bool IsGrounded()
    {
        // Check if the ground check box overlaps with the ground layer
        return Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the brick has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Kill the player instantly
            PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
            if (player != null)
            {
                player.TakeDamage(player.maxHealth);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckBox);
        }
    }
}