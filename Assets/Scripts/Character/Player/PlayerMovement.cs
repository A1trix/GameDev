using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Base movement speed
    public float airControlMultiplier = 0.5f; // Speed multiplier when in the air
    public float joystickThreshold = 0.2f; // Minimum joystick input to start moving
    public LayerMask groundLayer; // Layer mask for ground detection
    public PlayerJump playerJump;
    public Joystick joystick; // Reference to the joystick

    [Header("Knockback Settings")]
    public float knockbackForce = 10f; // Force applied during knockback
    public float knockbackDuration = 0.5f; // Duration of knockback effect
    public float knockbackCounter; // Tracks remaining knockback time
    public bool knockbackFromRight; // Direction of knockback

    [Header("Camera Settings")]
    public Camera mainCamera; // Reference to the main camera
    public Vector3 cameraOffset = new Vector3(0f, 2f, -10f); // Camera offset from the player
    public float followSpeed = 5f; // Speed at which the camera follows the player

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    // private bool isGrounded; // Tracks if the player is grounded

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>(); // Find the joystick in the scene
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Auto-assign the main camera if not set
        }
    }

    private void Update()
    {
        HandleMovement();
        FollowPlayerWithCamera();
    }

    public void HandleMovement()
    {
        if (knockbackCounter <= 0)
        {
            float horizontalInput = joystick.Horizontal;

            // Check if joystick input exceeds the threshold
            if (Mathf.Abs(horizontalInput) >= joystickThreshold)
            {
                // Normalize the input to ensure consistent speed
                horizontalInput = Mathf.Sign(horizontalInput);

                // Apply movement speed
                float speed = playerJump.IsGrounded() ? moveSpeed : moveSpeed * airControlMultiplier;
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
            else
            {
                // Stop movement if joystick input is below the threshold
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        else
        {
            // Apply knockback
            rb.velocity = new Vector2(
                knockbackFromRight ? -knockbackForce : knockbackForce,
                knockbackForce
            );
            knockbackCounter -= Time.deltaTime;
        }
    }

    public void ApplyKnockback(bool fromRight)
    {
        knockbackFromRight = fromRight;
        knockbackCounter = knockbackDuration;
    }

    public void FollowPlayerWithCamera()
    {
        if (mainCamera != null)
        {
            Vector3 targetPosition = transform.position + cameraOffset;
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the ground check radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}