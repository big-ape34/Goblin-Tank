using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject chargerPrefab;
    public GameObject orbiterPrefab;
    public GameObject flankerPrefab;
    public float timeBetweenWaves = 8f;
    public int chargersPerWave = 3;
    public int orbitersPerWave = 1;
    public int flankersPerWave = 1;
    public int flankerUnlockKills = 15;
    public Transform tankTransform;

    [Header("Spawn Settings")]
    public float spawnBufferMin = 1f;
    public float spawnBufferMax = 3f;

    const float WorldHalfSize = 100f;

    float waveTimer;
    public int TotalKills { get; private set; }
    bool flankersUnlocked;
    bool spawningEnabled = true;
    TurretController turretController;

    public int waveCount;

    void Start()
    {
        waveCount = 1;

        if (tankTransform == null)
        {
            GameObject tank = GameObject.FindGameObjectWithTag("Tank");
            if (tank != null)
                tankTransform = tank.transform;
        }

        turretController = FindObjectOfType<TurretController>();

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

    void HandleEnemyKilled(EnemyBase enemy)
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
            waveCount++;
        }
    }

    void SpawnWave()
    {
        if (tankTransform == null)
            return;

        for (int i = 0; i < chargersPerWave; i++)
        {
            Vector3 pos = GetOffScreenSpawnPosition();
            if (chargerPrefab != null)
                Instantiate(chargerPrefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < orbitersPerWave; i++)
        {
            Vector3 pos = GetOffScreenSpawnPosition();
            if (orbiterPrefab != null)
                Instantiate(orbiterPrefab, pos, Quaternion.identity);
        }

        if (flankersUnlocked)
        {
            for (int i = 0; i < flankersPerWave; i++)
            {
                Vector3 pos = GetFlankerSpawnPosition();
                if (flankerPrefab != null)
                    Instantiate(flankerPrefab, pos, Quaternion.identity);
            }
        }
    }

    Vector3 GetOffScreenSpawnPosition()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        Vector3 camPos = cam.transform.position;

        float buffer = Random.Range(spawnBufferMin, spawnBufferMax);
        int edge = Random.Range(0, 4); // 0=top, 1=bottom, 2=left, 3=right

        float x, y;

        switch (edge)
        {
            case 0: // top
                x = Random.Range(camPos.x - halfWidth, camPos.x + halfWidth);
                y = camPos.y + halfHeight + buffer;
                break;
            case 1: // bottom
                x = Random.Range(camPos.x - halfWidth, camPos.x + halfWidth);
                y = camPos.y - halfHeight - buffer;
                break;
            case 2: // left
                x = camPos.x - halfWidth - buffer;
                y = Random.Range(camPos.y - halfHeight, camPos.y + halfHeight);
                break;
            default: // right
                x = camPos.x + halfWidth + buffer;
                y = Random.Range(camPos.y - halfHeight, camPos.y + halfHeight);
                break;
        }

        x = Mathf.Clamp(x, -WorldHalfSize, WorldHalfSize);
        y = Mathf.Clamp(y, -WorldHalfSize, WorldHalfSize);

        return new Vector3(x, y, 0f);
    }

    Vector3 GetFlankerSpawnPosition()
    {
        if (turretController == null)
            return GetOffScreenSpawnPosition();

        Vector2 aimDir = turretController.AimDirection;

        for (int attempt = 0; attempt < 10; attempt++)
        {
            Vector3 pos = GetOffScreenSpawnPosition();
            Vector2 toSpawn = ((Vector2)pos - (Vector2)tankTransform.position).normalized;
            float angle = Vector2.Angle(aimDir, toSpawn);

            if (angle > 60f) // outside the 120-degree cone (60 each side)
                return pos;
        }

        // Fallback: spawn directly behind the turret aim direction
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        float spawnDist = Mathf.Max(halfWidth, halfHeight) + Random.Range(spawnBufferMin, spawnBufferMax);

        Vector2 behindDir = -aimDir.normalized;
        float x = Mathf.Clamp(tankTransform.position.x + behindDir.x * spawnDist, -WorldHalfSize, WorldHalfSize);
        float y = Mathf.Clamp(tankTransform.position.y + behindDir.y * spawnDist, -WorldHalfSize, WorldHalfSize);

        return new Vector3(x, y, 0f);
    }
}
