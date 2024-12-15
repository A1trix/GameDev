using UnityEngine;
using System.Collections.Generic;

// TODO
public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Spawning Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int initialEnemyCount = 5;

    private List<EnemyBase> enemies = new List<EnemyBase>();

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (enemies.Count < initialEnemyCount)
            {
                GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                EnemyBase enemy = enemyObject.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemies.Add(enemy);
                }
            }
        }
    }

    public void RemoveEnemy(EnemyBase enemy)
    {
        enemies.Remove(enemy);
    }

    // Call this in an update loop if you want to update all enemies
    private void Update()
    {
        foreach (EnemyBase enemy in enemies)
        {
            // Here, you can add custom updates per enemy, such as checking distance to the player
        }
    }
}
