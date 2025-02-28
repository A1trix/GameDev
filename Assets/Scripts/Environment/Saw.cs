using UnityEngine;

public class Saw : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 300f; // Speed at which the saw spins (degrees per second)

    [Header("Player Interaction")]
    [SerializeField] private LayerMask playerLayer; // Layer mask for the player

    private void Update()
    {
        // Rotate the saw endlessly
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the saw has collided with the player
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            // Kill the player instantly
            PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
            if (player != null)
            {
                player.TakeDamage(player.maxHealth); // Deal enough damage to kill the player
            }
        }
    }
}