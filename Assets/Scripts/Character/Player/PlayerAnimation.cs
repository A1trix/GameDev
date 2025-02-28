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
        // Moving animation
        float moveSpeed = Mathf.Abs(playerMovement.GetComponent<Rigidbody2D>().velocity.x);
        animator.SetFloat("moveSpeed", moveSpeed);

        // Jumping animation
        bool isGrounded = playerJump.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);
    }

    // Trigger animation for normal attack
    public void TriggerNormalAttackAnimation()
    {
        Debug.Log("Normal Attack Animation Triggered");
        animator.SetTrigger("attack");
    }

    // Trigger animation for ranged attack
    public void TriggerRangedAttackAnimation()
    {
        Debug.Log("Ranged Attack Animation Triggered");
        animator.SetTrigger("rangedAttack");
    }

    // Trigger animation for jump attack
    public void TriggerJumpAttackAnimation()
    {
        Debug.Log("Jump Attack Animation Triggered");
        animator.SetTrigger("jumpAttack");
    }
}