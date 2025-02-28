using UnityEngine;

// Not integrated
public class ChestAnimation : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component
    public bool isOpened = false; // Flag to track if the chest is already opened

    private void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Ensure the chest starts in the closed state
        if (animator != null)
        {
            animator.Play("Idle"); // Assuming "Closed" is the name of the idle state
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player entered the trigger and the chest is not already opened
        if (collision.CompareTag("Player") && !isOpened)
        {
            // Play the chest opening animation
            OpenChest();
        }
    }

    public void OpenChest()
    {
        // Set the flag to prevent the chest from opening multiple times
        isOpened = true;

        // Trigger the "Open" animation
        if (animator != null)
        {
            animator.SetTrigger("Open"); // Assuming "Open" is the trigger parameter
        }

        // You can add additional logic here, such as:
        // - Playing a sound effect
        // - Spawning loot
        // - Logging debug information
        Debug.Log("Chest opened!");
    }
}