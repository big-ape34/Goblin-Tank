using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject chargerPrefab;
    public GameObject orbiterPrefab;
    public GameObject flankerPrefab;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 8f;
    public int chargersPerWave = 3;
    public int orbitersPerWave = 1;
    public int flankersPerWave = 1;
    public int flankerUnlockKills = 15;
    public Transform tankTransform;

    float waveTimer;
    public int TotalKills { get; private set; }
    bool flankersUnlocked;
    bool spawningEnabled = true;

    void Start()
    {
        if (tankTransform == null)
        {
            GameObject tank = GameObject.FindGameObjectWithTag("Tank");
            if (tank != null)
                tankTransform = tank.transform;
        }

        SpawnWave();
    }

    void OnEnable()
    {
        EnemyBase.OnEnemyKilled += HandleEnemyKilled;
        PlayerHealth.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        EnemyBase.OnEnemyKilled -= HandleEnemyKilled;
        PlayerHealth.OnPlayerDeath -= HandlePlayerDeath;
    }

    void HandleEnemyKilled()
    {
        TotalKills++;
        if (TotalKills >= flankerUnlockKills)
            flankersUnlocked = true;
    }

    void HandlePlayerDeath()
    {
        spawningEnabled = false;
    }

    void Update()
    {
        if (!spawningEnabled)
            return;

        waveTimer += Time.deltaTime;
        if (waveTimer >= timeBetweenWaves)
        {
            waveTimer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (tankTransform == null)
            return;

        int flankerCount = flankersUnlocked ? flankersPerWave : 0;
        int totalEnemies = chargersPerWave + orbitersPerWave + flankerCount;

        for (int i = 0; i < totalEnemies; i++)
        {
            float angle = i * (360f / totalEnemies) + Random.Range(-15f, 15f);
            Vector3 spawnPosition = tankTransform.position + new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius,
                0f
            );

            GameObject prefab;
            if (i < chargersPerWave)
                prefab = chargerPrefab;
            else if (i < chargersPerWave + orbitersPerWave)
                prefab = orbiterPrefab;
            else
                prefab = flankerPrefab;

            if (prefab != null)
                Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
