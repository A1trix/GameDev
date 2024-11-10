using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    public void UpdateAnimation()
    {
        // moving animation
        float moveSpeed = Mathf.Abs(playerMovement.GetComponent<Rigidbody2D>().velocity.x);
        animator.SetFloat("moveSpeed", moveSpeed);

        // jumping animation
        bool isGrounded = playerJump.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);


        // ...
    }
}
