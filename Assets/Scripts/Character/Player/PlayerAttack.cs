using UnityEngine;

[System.Serializable]
public struct AttackType
{
    public string animationName;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;             
    public GameObject projectilePrefab;
    public float projectileSpeed;
}

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public LayerMask enemyLayers;
    public AttackType Attack;
    public AttackType RangedAttack;
    public AttackType JumpAttack;

    private PlayerAnimation playerAnimation;
    private EnemyBase enemyBase;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        enemyBase = GetComponent<EnemyBase>();
    }

    // Detects when enemies get hit by the attack
    public void HandleAttack(AttackType attack)
    {
       // Handle melee attacks
        if (attack.projectilePrefab == null)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack.attackPoint.position, attack.attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit " + enemy.name);
                enemyBase.TakeDamage(attack.attackDamage);
            }
        }
        else
        {
            // Handle ranged attacks
            ShootProjectile(attack);
        }
    }

    // TODO 
    private void ShootProjectile(AttackType attack)
    {
      // Instantiate the projectile at the attack point
        GameObject fireball = Instantiate(attack.projectilePrefab, attack.attackPoint.position, Quaternion.identity);

        // Set the fireball's velocity based on the player's facing direction
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        float direction = transform.localScale.x > 0 ? 1f : -1f;  // Determine if the player is facing right or left
        rb.velocity = new Vector2(direction * attack.projectileSpeed, 0);

        // Flip the fireball sprite if shooting left
        if (direction < 0)
        {
            Vector3 scale = fireball.transform.localScale;
            scale.x *= -1;
            fireball.transform.localScale = scale;
        }
    }

  // Visualization
private void OnDrawGizmosSelected()
{
    // Visualize Attack range
    if (Attack.attackPoint != null)
    {
        Gizmos.color = Color.red; // You can choose a color for the attack range
        Gizmos.DrawWireSphere(Attack.attackPoint.position, Attack.attackRange); // Draw range for Attack
    }

    // Visualize RangedAttack range
    if (RangedAttack.attackPoint != null)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(RangedAttack.attackPoint.position, RangedAttack.attackRange); // Draw range for RangedAttack
    }

    // Visualize JumpAttack range
    if (JumpAttack.attackPoint != null)
    {
        Gizmos.color = Color.green; 
        Gizmos.DrawWireSphere(JumpAttack.attackPoint.position, JumpAttack.attackRange); // Draw range for JumpAttack
    }
}

}
