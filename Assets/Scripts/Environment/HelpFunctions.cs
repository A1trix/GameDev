using UnityEngine;

public class HelpFunctions : MonoBehaviour
{
    [Header("Player Interaction")]
    [SerializeField] private LayerMask playerLayer; // Layer mask for the player

    public void DieOnTouch(Collision2D collision)
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