using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void UpdateAnimation()
    {
        // Moving animation
        float moveSpeed = Mathf.Abs(playerMovement.GetComponent<Rigidbody2D>().velocity.x);
        animator.SetFloat("moveSpeed", moveSpeed);

        // Jumping animation
        bool isGrounded = playerJump.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);

        // Attack animation
        HandleAttackAnimation();

        // ...
    }

    private void HandleAttackAnimation()
    {
        // Primary Attack (Left Mouse Button)
        if (Input.GetButtonDown("Fire1") && playerJump.IsGrounded())
        {
            Debug.Log("Attack Triggered");
            animator.SetTrigger("attack");
            // Passing attack type to the attack script
            // playerAttack.HandleAttack(playerAttack.Attack);
        }

        //TODO
        // Secondary Attack (Right Mouse Button) - Fireball
        if (Input.GetButtonDown("Fire2") && playerJump.IsGrounded())
        {
            Debug.Log("RangedAttack Triggered");
            animator.SetTrigger("rangedAttack");
            // playerAttack.HandleAttack(playerAttack.RangedAttack);
        }

        //TODO
        // Jump Attack (Left Mouse Button in Air)
        if (Input.GetButtonDown("Fire1") && !playerJump.IsGrounded())
        {
            Debug.Log("JumpAttack Triggered");

            animator.SetTrigger("jumpAttack");
            playerAttack.HandleAttack(playerAttack.JumpAttack);
        }
    }
}
