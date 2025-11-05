using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private float spawnDelay = 1f;

    [Header("Spawn Rate Scaling")]
    [SerializeField] private float minTimeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRateDecreasePerRound = 0.1f;

    private bool isSpawning = false;
    private int lastRound = 0;

    private void Update()
    {
        // Check if a new round has started
        if (RoundManager.Instance != null &&
            RoundManager.Instance.IsRoundActive &&
            RoundManager.Instance.CurrentRound != lastRound)
        {
            lastRound = RoundManager.Instance.CurrentRound;
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        if (isSpawning) return;

        int round = RoundManager.Instance.CurrentRound;
        Debug.Log($"EnemySpawner: Round {round} started! Enemies to spawn: {RoundManager.Instance.EnemiesToSpawn}");

        // Increase spawn rate as rounds progress
        float adjustedSpawnTime = Mathf.Max(
            minTimeBetweenSpawns,
            timeBetweenSpawns - (round * spawnRateDecreasePerRound)
        );

        Debug.Log($"EnemySpawner: Spawn interval: {adjustedSpawnTime}s");
        StartCoroutine(SpawnEnemies(adjustedSpawnTime));
    }

    private IEnumerator SpawnEnemies(float spawnInterval)
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        while (RoundManager.Instance.EnemiesToSpawn > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Enemy prefab or spawn points not set!");
            return;
        }

        // Choose random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Notify round manager
        RoundManager.Instance.EnemySpawned();

        Debug.Log($"Enemy spawned at {spawnPoint.name}. Remaining to spawn: {RoundManager.Instance.EnemiesToSpawn}");
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.DrawWireSphere(point.position, 0.5f);
            }
        }
    }
}