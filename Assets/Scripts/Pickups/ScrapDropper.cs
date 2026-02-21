using UnityEngine;

public class ScrapDropper : MonoBehaviour
{
    [SerializeField] private GameObject scrapPrefab;

    [Header("Drop Ranges")]
    [SerializeField] private int chargerDropMin = 10;
    [SerializeField] private int chargerDropMax = 25;
    [SerializeField] private int orbiterDropMin = 10;
    [SerializeField] private int orbiterDropMax = 25;
    [SerializeField] private int flankerDropMin = 35;
    [SerializeField] private int flankerDropMax = 60;

    void OnEnable()
    {
        EnemyBase.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        EnemyBase.OnEnemyKilled -= HandleEnemyKilled;
    }

    void HandleEnemyKilled(EnemyBase enemy)
    {
        if (scrapPrefab == null || enemy == null)
            return;

        int dropCount = GetDropCount(enemy);
        Vector3 deathPos = enemy.transform.position;

        for (int i = 0; i < dropCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.8f;
            Vector3 spawnPos = deathPos + (Vector3)offset;
            GameObject scrap = Instantiate(scrapPrefab, spawnPos, Quaternion.identity);
            ScrapPickup pickup = scrap.GetComponent<ScrapPickup>();
            if (pickup != null)
                pickup.Initialize(1);
        }
    }

    int GetDropCount(EnemyBase enemy)
    {
        if (enemy is EnemyCharger)
            return Random.Range(chargerDropMin, chargerDropMax + 1);
        if (enemy is EnemyOrbiter)
            return Random.Range(orbiterDropMin, orbiterDropMax + 1);
        if (enemy is EnemyFlanker)
            return Random.Range(flankerDropMin, flankerDropMax + 1);

        return Random.Range(chargerDropMin, chargerDropMax + 1);
    }
}
