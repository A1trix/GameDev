using UnityEngine;

public class RedBlobJump : EnemyBaseJump
{
    protected override void OnJump()
    {
        base.OnJump(); // Retain base jump functionality
        Debug.Log("RedBlob executed its unique jump behavior!");
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
