using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
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
