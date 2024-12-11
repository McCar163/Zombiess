using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Reference to the enemy prefab to spawn
    public Transform spawnPoint;          // The spawn point where the enemy will appear
    public float spawnInterval = 10f;     // Interval between each spawn in seconds

    private void Start()
    {
        // Start the spawning process using a coroutine
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)  // Infinite loop to spawn enemies continuously
        {
            // Spawn the enemy
            SpawnEnemy();

            // Wait for the specified interval (10 seconds)
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate the enemy prefab at the spawn point with no rotation (Quaternion.identity)
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Debug log to confirm the spawn
        Debug.Log("Spawned a new enemy at " + spawnPoint.position);
    }
}

