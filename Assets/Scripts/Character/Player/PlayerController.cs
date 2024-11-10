using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        playerMovement.HandleMovement();
        playerJump.HandleJumping();
        playerAnimation.UpdateAnimation();
    }
}
