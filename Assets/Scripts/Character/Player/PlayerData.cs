using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int level;
    // public int currentHealth;
    public int maxHealth;
    public int coins;
    public float[] position;
    public string sceneName;

    public PlayerData(PlayerBase player)
    {
        level = player.level;
        // currentHealth = player.currentHealth;
        maxHealth = player.maxHealth;
        coins = player.coinCount;
        position = new float[2];
        position[0] = player.spawnPoint.position.x;
        position[1] = player.spawnPoint.position.y;
        sceneName = SceneManager.GetActiveScene().name;
    }
}

// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// [System.Serializable]
// public class PlayerData {

// 	public int level;
//   public int health;
//   public int coins;
//   public float[] position;

//   public PlayerData(PlayerBase player)
//   {
//     level = player.level;
//     health = player.maxHealth;
//     coins = player.coinCount;

//     position = new float[2];
//     position[0] = player.transform.position.x;
//     position[1] = player.transform.position.y;
//   }
// }