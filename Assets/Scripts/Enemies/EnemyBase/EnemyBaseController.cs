using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyController : MonoBehaviour
{
    private EnemyBaseMovement enemyBaseMovement;

    private void Start()
    {
        enemyBaseMovement = GetComponent<EnemyBaseMovement>();
    }

    private void Update()
    {
        enemyBaseMovement.HandleMovement();
    }
}
