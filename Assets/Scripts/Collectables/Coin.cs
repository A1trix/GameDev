using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public PlayerBase player; // Reference to the player
    public int value = 1; // Value of the coin (default is 1)
    [SerializeField] private TextMeshProUGUI coinText; // UI element for displaying coin count

    // Initializes UI and loads saved coin count
    private void Start()
    {
        UpdateCoinUI();
        player.LoadPlayerCoins();
    }

    // Updates the coin UI every frame (potentially redundant)
    private void Update()
    {
        UpdateCoinUI();
    }

    // Detects collision with the player and collects the coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBase player = collision.GetComponent<PlayerBase>();
            if (player != null)
            {
                CollectCoin(value);
            }

            Destroy(gameObject); // Remove the coin from the scene
        }
    }

    // Adds the coin value to the player's total and updates the UI
    public void CollectCoin(int value)
    {
        player.coinCount += value;
        UpdateCoinUI();
        SaveSystem.SavePlayer(player); // Saves the updated coin count
        Debug.Log("Coin Count: " + player.coinCount);
    }

    // Updates the coin UI text with the current coin count
    public void UpdateCoinUI()
    {
        if (coinText != null) coinText.text = $"{player.coinCount}";
    }
}
