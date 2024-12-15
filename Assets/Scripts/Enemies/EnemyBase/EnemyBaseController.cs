using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    private EnemyBaseMovement enemyBaseMovement;
    private EnemyBaseJump enemyBaseJump;

    private void Start()
    {
        enemyBaseMovement = GetComponent<EnemyBaseMovement>();
        enemyBaseJump = GetComponent<EnemyBaseJump>();
    }

    private void Update()
    {
        enemyBaseMovement.HandleMovement();
        // enemyBaseJump.PerformJump();
    }
}
