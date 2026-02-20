using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject chargerPrefab;
    public GameObject orbiterPrefab;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 8f;
    public int chargersPerWave = 3;
    public int orbitersPerWave = 1;
    public Transform tankTransform;

    float waveTimer;

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

    void Update()
    {
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

        int totalEnemies = chargersPerWave + orbitersPerWave;

        for (int i = 0; i < totalEnemies; i++)
        {
            float angle = i * (360f / totalEnemies) + Random.Range(-15f, 15f);
            Vector3 spawnPosition = tankTransform.position + new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius,
                0f
            );

            GameObject prefab = i < chargersPerWave ? chargerPrefab : orbiterPrefab;
            if (prefab != null)
                Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
