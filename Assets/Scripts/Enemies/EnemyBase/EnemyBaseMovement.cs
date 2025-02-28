using UnityEngine;

public class EnemyBaseMovement : MonoBehaviour
{
    private EnemyFlip enemyFlip;
    public GameObject Player;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public bool isPatrolling = true;

    [Header("Patrol Settings")]
    public Vector2 leftBoundary = new Vector2(-5f, 0f);
    public Vector2 rightBoundary = new Vector2(5f, 0f);

    private bool movingRight = true;

    [Header("Player Detection")]
    public float detectionRange = 5f;
    private bool isChasing = false;

    private void Start()
    {
        enemyFlip = GetComponent<EnemyFlip>();
        rb = GetComponent<Rigidbody2D>();

        // Validate boundaries
        if (leftBoundary == null || rightBoundary == null)
        {
            Debug.LogError("Patrol boundaries are not set. Assign left and right boundary Transforms.");
        }
    }

    // TODO: Split detection into function
    public void HandleMovement()
    {
        // Check if the player is within the detection range
        isChasing = Vector2.Distance(transform.position, Player.transform.position) <= detectionRange;

        // If chasing, follow the player. Otherwise, patrol.
        if (isChasing)
        {
            FollowPlayer();
        }
        else if (isPatrolling)
        {
            // Debug.Log("Patrolling");
            Patrol();
        }
    }


    // Handles patrolling behavior between set boundaries.
    private void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            // Debug.Log("Moving Right");

            // Flip the enemy to the left if it's moving right
            if (!enemyFlip.facingRight)
            {
                enemyFlip.FlipEnemy();
            }

            // Check if the enemy has reached the right boundary
            if (transform.position.x >= rightBoundary.x)
            {
                movingRight = false;
                // Debug.Log("Flipped enemy to the left");
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            // Debug.Log("Moving Left");

            // Flip the enemy to the right if it's moving left
            if (enemyFlip.facingRight)
            {
                enemyFlip.FlipEnemy();
            }

            // Check if the enemy has reached the left boundary
            if (transform.position.x <= leftBoundary.x)
            {
                movingRight = true;
                // Debug.Log("Flipped enemy to the right");
            }
        }
    }

    // Handles following behavior
    private void FollowPlayer()
    {
        // Check for direction of the player 
        float direction = Player.transform.position.x > transform.position.x ? 1f : -1f;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        // Flip the enemy based on the direction it's moving towards
        if (direction > 0 && !enemyFlip.facingRight)
        {
            enemyFlip.FlipEnemy();
        }
        else if (direction < 0 && enemyFlip.facingRight)
        {
            enemyFlip.FlipEnemy();
        }
    }

    /// Stops the enemy's movement.
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    /// Resumes patrolling.
    public void ResumePatrol()
    {
        isPatrolling = true;
    }


    /// Visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(leftBoundary, 0.2f);
        Gizmos.DrawWireSphere(rightBoundary, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftBoundary, rightBoundary);
    }
}
