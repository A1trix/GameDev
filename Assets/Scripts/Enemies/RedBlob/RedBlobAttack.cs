using UnityEngine;

public class RedBlobAttack : EnemyBaseAttack
{

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    public PlayerMovement playerMovement;
    protected override void PerformAttack(Collider2D target)
    {
        base.PerformAttack(target); // Retain base functionality (deal damage)
        Debug.Log("RedBlob is performing its unique attack!");

        // Knockback for Player
        playerMovement.knockbackCounter = playerMovement.knockbackDuration;
        if(target.transform.position.x <= transform.position.x)
        {
            playerMovement.knockbackFromRight = true;
        }
        if(target.transform.position.x >= transform.position.x)
        {
            playerMovement.knockbackFromRight = false;
        }
    }
}
