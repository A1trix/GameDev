using System.Data.Common;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    [Header("Player Configuration")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] private float respawnDelay = 0f;
    [SerializeField] public Transform spawnPoint;
    
    [Header("UI References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI coinText;

    public int currentHealth;
    public int coinCount;
    public int level = 1;
    private bool isDead;
    private Rigidbody2D rb;
    private Coin coin;
    private float levelStartTime;

    // Retrieves the Rigidbody2D component.
    // Calls InitializePlayerState() to set up the player's starting conditions.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializePlayerState();

        // Reset the timer when the level starts
        ResetTimer();
    }

    // Calls HandleDebugInput() every frame to check for debug key presses.
    private void Update()
    {
        HandleDebugInput();
    }

    // Sets currentHealth to maxHealth.
    // Initializes the health UI.
    // Checks if save data exists:
    // If yes, loads saved data.
    // If not, resets the player to default values.
    public void InitializePlayerState()
    {
        currentHealth = maxHealth;
        playerHealth.SetMaxHealth(maxHealth);

        if (SaveSystem.SaveFileExists())
        {
            LoadPlayer();
        }
        else
        {
            ResetToDefaultValues();
        }
    }

    // Reduces currentHealth, ensuring it doesn't go below 0.
    // Updates the UI health bar.
    // Calls Die() if health reaches 0.
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        playerHealth.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Sets isDead = true to prevent further damage.
    // Uses Invoke() to call HandleDeath() after respawnDelay.
    private void Die()
    {
        isDead = true;
        Invoke(nameof(HandleDeath), respawnDelay);
    }

    // Calls SceneController to restart the level.
    private void HandleDeath()
    {
        SceneController.Instance.HandlePlayerDeath();
    }

    // Increases coinCount.
    // Updates the UI.
    // Saves player progress.
    // public void CollectCoin(int value)
    // {
    //     coinCount += value;
    //     UpdateCoinUI();
    //     SaveSystem.SavePlayer(this);
    //     Debug.Log("Coin Count: " + coinCount);
    // }

    // Updates the coin counter UI.
    // private void UpdateCoinUI()
    // {
    //     if (coinText != null) coinText.text = $"{coinCount}";
    // }

    // Saves the state of the Player
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    // Loads saved player data.
    // If no data exists, it starts with default values.
    // Updates level and maxHealth from saved data.
    // Checks if the player is already in the correct scene:
    // If yes, it keeps the player there.
    // If not, transitions to the correct scene.
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.LogWarning("No saved data found. Starting with default values.");
            ResetToDefaultValues();
            return;
        }

        // Update player stats from saved data
        level = data.level;
        maxHealth = data.maxHealth;
        // currentHealth = data.currentHealth;
        coinCount = data.coins;

        // Set position
        Vector2 loadedPosition = new Vector2(data.position[0], data.position[1]);
        transform.position = loadedPosition;

        // Update UI
        playerHealth.SetMaxHealth(maxHealth);
        playerHealth.SetHealth(currentHealth);
    }

    public void LoadPlayerCoins()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.LogWarning("No saved data found. Starting with default values.");
            ResetToDefaultValues();
            return;
        }

        coinCount = data.coins;
    }

    private void ResetTimer()
    {
        // Reset the level timer
        levelStartTime = Time.time;
    }

    public float GetElapsedTime()
    {
        // Calculate the elapsed time since the level started
        return Time.time - levelStartTime;
    }

    // private void LoadPlayer()
    // {
    //     // Load saved player data
    //     PlayerData data = SaveSystem.LoadPlayer();
    //     if (data == null)
    //     {
    //         Debug.LogWarning("No saved data found. Starting with default values.");
    //         ResetToDefaultValues();
    //         return;
    //     }

    //     // Update player stats from saved data
    //     level = data.level;
    //     maxHealth = data.health;

    //     Vector2 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];

    // }

    // !!! DEBUG Not for use in production !!!
    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Z)) TakeDamage(34);
        if (Input.GetKeyDown(KeyCode.P)) DeleteSaveData();
    }

    // !!! DEBUG Not for use in production !!!
    private void DeleteSaveData()
    {
        SaveSystem.DeleteSaveData();
        ResetToDefaultValues();
    }

    // Resets all player stats.
    // Moves the player to (0,0).
    // Resets health UI.
    public void ResetToDefaultValues()
    {
        level = 1;
        maxHealth = 100;
        currentHealth = maxHealth;
        coinCount = 0;

        // Spawn the player at the default position in the first level
        transform.position = spawnPoint.position; // Replace with your default spawn position

        // Update UI elements
        // coin.UpdateCoinUI();
        playerHealth.SetMaxHealth(maxHealth);
        ResetTimer();

        Debug.Log("Starting with default values.");
    }
}