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
    private float levelStartTime;

    // Initializes the player on start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializePlayerState();
        ResetTimer();
    }

    // Checks for debug input each frame
    private void Update()
    {
        HandleDebugInput();
    }

    // Initializes player state (health, save data, etc.)
    public void InitializePlayerState()
    {
        currentHealth = maxHealth;
        playerHealth.SetMaxHealth(maxHealth);

        if (SaveSystem.SaveFileExists())
            LoadPlayer();
        else
            ResetToDefaultValues();
    }

    // Handles taking damage and checking for death
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
        playerHealth.SetHealth(currentHealth);
        
        if (currentHealth <= 0)
            Die();
    }

    // Handles player death and triggers respawn
    private void Die()
    {
        isDead = true;
        Invoke(nameof(HandleDeath), respawnDelay);
    }

    // Calls SceneController to restart the level
    private void HandleDeath()
    {
        SceneController.Instance.HandlePlayerDeath();
    }

    // Saves player data
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    // Loads player data (health, coins, position, etc.)
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.LogWarning("No saved data found. Starting with default values.");
            ResetToDefaultValues();
            return;
        }

        level = data.level;
        maxHealth = data.maxHealth;
        coinCount = data.coins;

        transform.position = new Vector2(data.position[0], data.position[1]);
        
        playerHealth.SetMaxHealth(maxHealth);
        playerHealth.SetHealth(currentHealth);
    }

    // Loads only player coin data
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

    // Resets the level timer
    private void ResetTimer()
    {
        levelStartTime = Time.time;
    }

    // Returns elapsed time since level start
    public float GetElapsedTime()
    {
        return Time.time - levelStartTime;
    }

    // Resets player stats and position to default
    public void ResetToDefaultValues()
    {
        level = 1;
        maxHealth = 100;
        currentHealth = maxHealth;
        coinCount = 0;
        transform.position = spawnPoint.position;
        playerHealth.SetMaxHealth(maxHealth);
        ResetTimer();
        Debug.Log("Starting with default values.");
    }

    // Debugging: Allows quick testing of damage and save deletion
    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Z)) TakeDamage(34);
        if (Input.GetKeyDown(KeyCode.P)) DeleteSaveData();
    }

    // Debugging: Deletes save data and resets player
    private void DeleteSaveData()
    {
        SaveSystem.DeleteSaveData();
        ResetToDefaultValues();
    }
}
