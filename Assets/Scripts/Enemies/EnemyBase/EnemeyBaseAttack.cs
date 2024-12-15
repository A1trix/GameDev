using UnityEngine;

public class EnemyBaseAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public Vector2 attackRange = new Vector2(1.0f, 1.0f);
    public float attackCooldown = 1.5f;
    public LayerMask targetLayer;
    private float lastAttackTime = 0;

    protected virtual void Update()
    {
        HandleAttack();
    }

    // TODO (check method for functionality)
    protected virtual void HandleAttack()
    {
        // Check if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Collider2D target = DetectTarget();
            if (target != null)
            {
                PerformAttack(target);
                lastAttackTime = Time.time; // Reset the attack timer
            }
        }
    }

    // TODO (check method for functionality)
    // Detect a target in range
    protected virtual Collider2D DetectTarget()
    {
        // OverlapBox detects a target within the attack range
        Vector2 center = (Vector2)transform.position;
        Collider2D detectedTarget = Physics2D.OverlapBox(center, attackRange, 0f, targetLayer);

        if (detectedTarget != null)
        {
            Debug.Log($"{gameObject.name} detected {detectedTarget.name} within attack range.");
        }

        return detectedTarget;
    }

    // TODO (check method for functionality)
    // Perform attack (damage player)
    protected virtual void PerformAttack(Collider2D target)
    {
        Debug.Log($"{gameObject.name} attacked {target.name} for {attackDamage} damage.");

        // Call a method on the target to apply damage
        target.GetComponent<PlayerBase>()?.TakeDamage(attackDamage);
    }

    // Visualization
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, attackRange);
    }
}
