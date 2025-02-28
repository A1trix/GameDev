using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;

    // Initializes component references
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Handles jumping and animation updates each frame
    private void Update()
    {
        playerJump.HandleJumping();
        playerAnimation.UpdateAnimation();
    }

    // Handles movement and camera following using physics updates
    private void FixedUpdate()
    {
        playerMovement.HandleMovement();
        playerMovement.FollowPlayerWithCamera();
    }
}
