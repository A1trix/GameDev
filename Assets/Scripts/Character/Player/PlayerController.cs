using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();

    }

    private void Update()
    {
        playerJump.HandleJumping();
        playerAnimation.UpdateAnimation();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleMovement();
        playerMovement.FollowPlayerWithCamera();
    }
}
