using UnityEngine;
using System;

public class Fireball : MonoBehaviour
{
    public float speed = 10f; // Speed of the fireball
    public int damage = 10; // Damage dealt by the fireball
    public LayerMask enemyLayers; // Layers that the fireball can damage
    public LayerMask groundLayers; // Layers that the fireball should collide with (e.g., walls)
    public Rigidbody2D rb;
    public GameObject impactEffect;
    private Vector2 direction; // Direction the fireball is moving

    // Event that triggers when the fireball is destroyed
    public event Action<Fireball> OnFireballDestroyed;

    private bool isDestroyed = false; // Flag to prevent multiple triggers

    private void Start()
    {
        // Destroy the fireball after a set lifetime to prevent it from existing indefinitely
        // Destroy(gameObject, 5f); // Adjust the lifetime as needed
        isDestroyed = false;
    }

    public void Initialize(Vector2 shootDirection, float projectileSpeed, int fireballDamage, LayerMask enemyMask, LayerMask groundMask)
    {
        direction = shootDirection;
        speed = projectileSpeed;
        damage = fireballDamage;
        enemyLayers = enemyMask;
        groundLayers = groundMask;

        // Set the fireball's velocity
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is missing on the fireball prefab.");
        }

        // Flip the fireball sprite if shooting left
        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

private void OnTriggerEnter2D(Collider2D hitInfo)
{
    // Log the name and layer of the object the fireball collided with
    Debug.Log($"Fireball collided with: {hitInfo.name} on layer: {LayerMask.LayerToName(hitInfo.gameObject.layer)}");
    // Ignore collisions with the player and other fireballs
    if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Fireball"))
    {
        Debug.Log("Ignoring collision with player or another fireball.");
        return; // Exit the method without doing anything
    }
    // Check if the fireball collided with an enemy
    EnemyBase enemy = hitInfo.GetComponent<EnemyBase>();
    Debug.Log("Fireball hit an enemy.");
    // Try to get the EnemyBase component from the hit object
    if (enemy != null)
    {
      // Apply damage to the enemy
        enemy.TakeDamage(damage);
    }

    GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
    Destroy(effect, 0.2f);

    // Notify subscribers that the fireball is being destroyed
    TriggerDestroyEvent();

    // Destroy the fireball on impact
    // Destroy(gameObject); // Only destroy the fireball
    gameObject.SetActive(false);
}

    // private void OnDestroy()
    // {
    //     // Notify subscribers that the fireball is being destroyed
    //     TriggerDestroyEvent();
    // }

    private void TriggerDestroyEvent()
    {
        if (!isDestroyed) // Ensure the event is only triggered once
        {
            isDestroyed = true;
            OnFireballDestroyed?.Invoke(this);
        }
    }
}