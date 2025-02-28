using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyName;
    public int health = 100;
    public int damage = 10;

    protected Rigidbody2D rb;
    protected Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
