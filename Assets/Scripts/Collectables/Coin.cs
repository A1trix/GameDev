using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public PlayerBase player;
    public int value = 1; // Value of the coin (default is 1)
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        UpdateCoinUI();
        player.LoadPlayerCoins();
    }

    private void Update()
    {
        UpdateCoinUI();
        // player.LoadPlayerCoins();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collided with the coin
        if (collision.CompareTag("Player"))
        {
            // Get the Player component and add the coin value
            PlayerBase player = collision.GetComponent<PlayerBase>();
            if (player != null)
            {
                CollectCoin(value);
            }

            // Destroy the coin
            Destroy(gameObject);
        }
    }

    public void CollectCoin(int value)
    {
        player.coinCount += value;
        UpdateCoinUI();
        SaveSystem.SavePlayer(this.player);
        Debug.Log("Coin Count: " + player.coinCount);
    }

    public void UpdateCoinUI()
    {
        if (coinText != null) coinText.text = $"{player.coinCount}";
    }
}