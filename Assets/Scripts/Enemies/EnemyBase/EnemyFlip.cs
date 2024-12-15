using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    public bool facingRight = true;

    public void FlipEnemy()
    {
        // Only flip if the current direction needs to be changed
        facingRight = !facingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invert the scale on the X-axis to flip the sprite
        transform.localScale = localScale;
    }
}
