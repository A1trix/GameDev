using UnityEngine;

public class EnemyBaseAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10; // Damage dealt per attack
    public Vector2 attackRange = new Vector2(1.0f, 1.0f); // Area of attack
    public float attackCooldown = 1.5f; // Time between attacks
    public LayerMask targetLayer; // Defines what objects can be attacked
    private float lastAttackTime = 0; // Tracks last attack time

    // Continuously checks if the enemy can attack
    protected virtual void Update()
    {
        HandleAttack();
    }

    // Checks cooldown and attacks if a target is detected
    protected virtual void HandleAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D target = DetectTarget();
            if (target != null)
            {
                PerformAttack(target);
                lastAttackTime = Time.time;
            }
        }
    }

    // Detects if a target is within attack range
    protected virtual Collider2D DetectTarget()
    {
        Vector2 center = (Vector2)transform.position;
        return Physics2D.OverlapBox(center, attackRange, 0f, targetLayer);
    }

    // Applies damage to the detected target
    protected virtual void PerformAttack(Collider2D target)
    {
        Debug.Log($"{gameObject.name} attacked {target.name} for {attackDamage} damage.");
        target.GetComponent<PlayerBase>()?.TakeDamage(attackDamage);
    }

    // Visualizes the attack range in the editor
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, attackRange);
    }
}
