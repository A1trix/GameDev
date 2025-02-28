using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int level;       // Stores the player's current level
    public int maxHealth;   // Stores the player's maximum health
    public int coins;       // Stores the player's collected coins
    public float[] position; // Stores the player's position (x, y)
    public string sceneName; // Stores the current scene name for loading

    // Constructor to save player data from PlayerBase
    public PlayerData(PlayerBase player)
    {
        level = player.level;
        maxHealth = player.maxHealth;
        coins = player.coinCount;

        // Saves the player's spawn position
        position = new float[2];
        position[0] = player.spawnPoint.position.x;
        position[1] = player.spawnPoint.position.y;

        // Saves the active scene name
        sceneName = SceneManager.GetActiveScene().name;
    }
}