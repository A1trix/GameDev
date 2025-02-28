using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private bool facingRight = true;
    public PlayerMovement player;

    private void Update()
    {
        FlipCharacter();
    }

    private void FlipCharacter()
    {
        float horizontalInput = player.joystick.Horizontal;

        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
