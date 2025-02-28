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

    // Initializes component references
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>(); // Find the joystick in the scene

        // Auto-assign the main camera if not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Handles movement and camera follow each frame
    private void Update()
    {
        HandleMovement();
        FollowPlayerWithCamera();
    }

    // Processes player movement based on joystick input
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

                // Apply movement speed based on whether the player is grounded
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
            // Apply knockback effect
            rb.velocity = new Vector2(
                knockbackFromRight ? -knockbackForce : knockbackForce,
                knockbackForce
            );
            knockbackCounter -= Time.deltaTime;
        }
    }

    // Applies a knockback effect to the player
    public void ApplyKnockback(bool fromRight)
    {
        knockbackFromRight = fromRight;
        knockbackCounter = knockbackDuration;
    }

    // Makes the camera smoothly follow the player
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

    // Draws a ground check radius in the Unity Editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
