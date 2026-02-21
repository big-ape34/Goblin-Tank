using UnityEngine;

public class OrbiterAttack : MonoBehaviour, IEnemyAttack
{
    public static event System.Action OnOrbiterFired;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireInterval = 3f;
    [SerializeField] private float missileSpeed = 5f;
    [SerializeField] private GameObject enemyMissilePrefab;

    public float Damage => damage;

    private Transform target;
    private float fireTimer;

    public void InitAttack(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        if (target == null)
        {
            GameObject tank = GameObject.FindGameObjectWithTag("Tank");
            if (tank != null)
                target = tank.transform;
        }

        fireTimer = fireInterval;
    }

    void Update()
    {
        if (target == null)
            return;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = fireInterval;
            FireMissile();
        }
    }

    private void FireMissile()
    {
        if (enemyMissilePrefab == null)
            return;

        Vector3 directionToTarget = (target.position - transform.position).normalized;

        GameObject missile = Instantiate(enemyMissilePrefab, transform.position, Quaternion.identity);
        EnemyMissile missileScript = missile.GetComponent<EnemyMissile>();
        if (missileScript != null)
        {
            missileScript.speed = missileSpeed;
            missileScript.damage = damage;
            missileScript.direction = directionToTarget;
        }

        OnOrbiterFired?.Invoke();
    }
}
