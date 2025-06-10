using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnRate;
    }

    [Header("Wave Settings")]
    public List<Wave> waves;
    public Transform[] spawnPoints;

    [Header("UI")]
    public TMP_Text waveText;

    private int currentWaveIndex = 0;
    private int enemiesRemaining = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        isSpawning = true;

        Wave wave = waves[currentWaveIndex];
        enemiesRemaining = wave.enemyCount;

        UpdateWaveUI();

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject prefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

       
        var drone = enemy.GetComponent<EnemyDrone>();
        if (drone != null)
        {
            drone.OnDeath += OnEnemyKilled;
        }
    }

    void OnEnemyKilled()
    {
        enemiesRemaining--;
        UpdateWaveUI();

        if (enemiesRemaining <= 0 && !isSpawning)
        {
            currentWaveIndex++;

            if (currentWaveIndex < waves.Count)
            {
                StartCoroutine(StartNextWave());
            }
            else
            {
                if (waveText != null)
                {
                    waveText.text = " All Waves Complete!";
                }
            }
        }
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWaveIndex + 1} - Enemies Left: {enemiesRemaining}";
        }
    }
}
