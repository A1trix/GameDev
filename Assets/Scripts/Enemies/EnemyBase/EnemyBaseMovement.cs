using UnityEngine;

public class EnemyBaseMovement : MonoBehaviour
{
    private EnemyFlip enemyFlip;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public bool isPatrolling = true;

    [Header("Patrol Settings")]
    public Transform leftBoundary;
    public Transform rightBoundary;
    private bool movingRight = true;

    [Header("Player Detection")]
    public GameObject Player;
    public float detectionRange = 5f;
    private bool isChasing = false;

    private void Start()
    {
        enemyFlip = GetComponent<EnemyFlip>();
        rb = GetComponent<Rigidbody2D>();

        if (leftBoundary == null || rightBoundary == null)
        {
            Debug.LogError("Patrol boundaries are not set. Assign left and right boundary Transforms.");
        }

        rb.drag = 0; // Ensure smooth movement
    }

    private void Update()
    {
        DetectPlayer();
        HandleMovement();
    }

    // Prüft, ob der Spieler in Reichweite ist
    private void DetectPlayer()
    {
        if (Player != null)
        {
            isChasing = Vector2.Distance(transform.position, Player.transform.position) <= detectionRange;
        }
        else
        {
            isChasing = false;
        }
    }

    public void HandleMovement()
    {
        if (isChasing)
        {
            FollowPlayer();
        }
        else if (isPatrolling)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (!enemyFlip.facingRight) enemyFlip.FlipEnemy();

            if (transform.position.x >= rightBoundary.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (enemyFlip.facingRight) enemyFlip.FlipEnemy();

            if (transform.position.x <= leftBoundary.position.x)
            {
                movingRight = true;
            }
        }
    }

    private void FollowPlayer()
    {
        if (Player == null) return;

        float direction = Player.transform.position.x > transform.position.x ? 1f : -1f;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if ((direction > 0 && !enemyFlip.facingRight) || (direction < 0 && enemyFlip.facingRight))
        {
            enemyFlip.FlipEnemy();
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    public void ResumePatrol()
    {
        isPatrolling = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (leftBoundary != null && rightBoundary != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(leftBoundary.position, 0.2f);
            Gizmos.DrawWireSphere(rightBoundary.position, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftBoundary.position, rightBoundary.position);
        }
    }
}
