using Unity.VisualScripting;
using UnityEngine;

public class RedBlobAnimation : MonoBehaviour
{
    public Animator animator;
    private EnemyBaseMovement enemyBaseMovement;
    private RedBlobJump redBlobJump;

    private void Start()
    {
      enemyBaseMovement = GetComponent<EnemyBaseMovement>();
      redBlobJump = GetComponent<RedBlobJump>();
    }

    public void UpdateAnimation()
    {
        // Moving animation
        float moveSpeed = Mathf.Abs(enemyBaseMovement.GetComponent<Rigidbody2D>().velocity.x);
        animator.SetFloat("moveSpeed", moveSpeed);

        // Jumping animation
        bool isGrounded = redBlobJump.IsGrounded();
        animator.SetBool("isGrounded", isGrounded);
    }
}
