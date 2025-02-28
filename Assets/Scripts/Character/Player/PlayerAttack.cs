using UnityEngine;

[System.Serializable]
public struct AttackType
{
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public GameObject fireBallPrefab;
    public float projectileSpeed;
}

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public LayerMask enemyLayers;
    public AttackType Attack;
    public AttackType RangedAttack;
    public AttackType JumpAttack;

    private PlayerJump playerJump;
    private PlayerAnimation playerAnimation;
    private float lastShootTime = 0f; // Time when the last fireball was shot
    private float shootCooldown = 0.2f; // Cooldown between fireballs

    private void Start()
    {
        playerJump = GetComponent<PlayerJump>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Public method for normal attack (can be called from UI button)
    public void PerformNormalAttack()
    {
        if (!playerJump.IsGrounded())
        {
            playerAnimation.TriggerJumpAttackAnimation();
            PerformAttack(JumpAttack);
            Debug.Log("Jump Attack");
        }
        else
        {
            playerAnimation.TriggerNormalAttackAnimation();
            PerformAttack(Attack);  
        }
    }

    // Public method for ranged attack (can be called from UI button)
    public void PerformRangedAttack()
    {
        playerAnimation.TriggerRangedAttackAnimation();
        PerformAttack(RangedAttack);
    }

    // Common method to perform an attack
    private void PerformAttack(AttackType attack)
    {
        if (attack.fireBallPrefab == null)
        {
            // Melee Attack Logic
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack.attackPoint.position, attack.attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.TryGetComponent<EnemyBase>(out EnemyBase enemyComponent))
                {
                    Debug.Log($"Hit {enemy.name} for {attack.attackDamage} damage.");
                    enemyComponent.TakeDamage(attack.attackDamage);
                }
            }
        }
        else
        {
            // Ranged Attack Logic
            ShootFireBall(attack);
        }
    }

    private void ShootFireBall(AttackType attack)
    {
        // Check if the fireBallPrefab is assigned
        if (attack.fireBallPrefab == null)
        {
            Debug.LogError("Fireball prefab is not assigned in the AttackType.");
            return;
        }

        // Check if the cooldown has passed
        if (Time.time - lastShootTime < shootCooldown)
        {
            Debug.Log("Fireball is on cooldown.");
            return;
        }

        // Update the last shoot time
        lastShootTime = Time.time;

        // Instantiate the projectile at the attack point
        GameObject fireball = ObjectPool.instance.GetPoolObject();

        // Check if the fireball was instantiated successfully
        if (fireball == null)
        {
            Debug.LogError("Failed to instantiate fireball.");
            return;
        }

        fireball.SetActive(true);
        fireball.transform.position = attack.attackPoint.position;
        fireball.transform.rotation = Quaternion.identity;

        // Get the Fireball component
        Fireball fireballScript = fireball.GetComponent<Fireball>();

        if (fireballScript != null)
        {
            // Determine the shooting direction based on the player's facing direction
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            Vector2 shootDirection = new Vector2(direction, 0);

            // Initialize the fireball with the necessary data
            fireballScript.Initialize(
                shootDirection,
                attack.projectileSpeed,
                attack.attackDamage,
                enemyLayers,
                LayerMask.GetMask("Ground") // Set the obstacle layer(s)
            );

            // Subscribe to the OnFireballDestroyed event
            fireballScript.OnFireballDestroyed += HandleFireballDestroyed;
        }
        else
        {
            Debug.LogError("Fireball script is missing on the fireball prefab.");
        }
    }

    // Callback method for when the fireball is destroyed
    private void HandleFireballDestroyed(Fireball fireball)
    {
        Debug.Log("Fireball was destroyed!");

        // Unsubscribe from the event
        fireball.OnFireballDestroyed -= HandleFireballDestroyed;

        // You can add additional logic here, such as:
        // - Playing a sound effect
        // - Spawning a particle effect
        // - Logging debug information
    }

    // Visualization
    private void OnDrawGizmosSelected()
    {
        // Visualize Attack range
        if (Attack.attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Attack.attackPoint.position, Attack.attackRange);
        }

        // Visualize RangedAttack range
        if (RangedAttack.attackPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(RangedAttack.attackPoint.position, RangedAttack.attackRange);
        }

        // Visualize JumpAttack range
        if (JumpAttack.attackPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(JumpAttack.attackPoint.position, JumpAttack.attackRange);
        }
    }
}