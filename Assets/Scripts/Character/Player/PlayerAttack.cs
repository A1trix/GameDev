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

    public void PerformRangedAttack()
    {
        playerAnimation.TriggerRangedAttackAnimation();
        PerformAttack(RangedAttack);
    }

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
            ShootFireBall(attack);
        }
    }

    private void ShootFireBall(AttackType attack)
    {
        if (attack.fireBallPrefab == null)
        {
            Debug.LogError("Fireball prefab is not assigned in the AttackType.");
            return;
        }

        if (Time.time - lastShootTime < shootCooldown)
        {
            Debug.Log("Fireball is on cooldown.");
            return;
        }

        lastShootTime = Time.time;

        GameObject fireball = ObjectPool.instance.GetPoolObject();

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
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            Vector2 shootDirection = new Vector2(direction, 0);

            fireballScript.Initialize(
                shootDirection,
                attack.projectileSpeed,
                attack.attackDamage,
                enemyLayers,
                LayerMask.GetMask("Ground")
            );

            fireballScript.OnFireballDestroyed += HandleFireballDestroyed;
        }
        else
        {
            Debug.LogError("Fireball script is missing on the fireball prefab.");
        }
    }

    private void HandleFireballDestroyed(Fireball fireball)
    {
        Debug.Log("Fireball was destroyed!");

        fireball.OnFireballDestroyed -= HandleFireballDestroyed;
    }

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