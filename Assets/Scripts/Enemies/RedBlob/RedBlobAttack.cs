using UnityEngine;

public class RedBlobAttack : EnemyBaseAttack
{
    protected override void PerformAttack(Collider2D target)
    {
        base.PerformAttack(target); // Retain base functionality
        Debug.Log("RedBlob is performing its unique attack!");

        // Add RedBlob-specific attack logic, like applying a status effect
        // Example: target.GetComponent<PlayerBase>()?.ApplyStatusEffect("Burning");
    }
}
