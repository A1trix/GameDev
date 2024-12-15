using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Death Settings")]
    public float respawnDelay = 2f; // TODO?

    [Header("UI Settings")]
    // public HealthBar healthBar; -> Reference to a health bar UI component (optional)
    private bool isDead = false;

    private void Start()
    {
        // Initialize health at the start of the game
        currentHealth = maxHealth;

        // TODO
        // Update health bar UI, if applicable
        // if (healthBar != null)
        //     healthBar.SetMaxHealth(maxHealth);
    }

    // Method to apply damage to the player.
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Prevent further damage if the player is already dead

        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Current Health: {currentHealth}");

        // TODO
        // Update health bar UI, if applicable
        // if (healthBar != null)
        //     healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle player death.
    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died!");

        // TODO
        // Optionally, disable player controls
        // GetComponent<PlayerMovement>()?.enabled = false;
        // GetComponent<PlayerAttack>()?.enabled = false;

        // Handle game over or respawn
        Invoke(nameof(HandleDeath), respawnDelay);
    }

    // TODO
    // Method to handle post-death logic (e.g., respawning).
    private void HandleDeath()
    {
        // Implement respawn logic or game over handling
        Debug.Log("Respawn or Game Over logic goes here.");
    }

    // TODO
    /// Method to heal the player.
    public void Heal(int healAmount)
    {
        if (isDead) return; // Prevent healing if the player is dead

        currentHealth += healAmount;

        // Clamp health to maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"{gameObject.name} healed by {healAmount}. Current Health: {currentHealth}");

        // Update health bar UI, if applicable
        // if (healthBar != null)
        //     healthBar.SetHealth(currentHealth);
    }
}
