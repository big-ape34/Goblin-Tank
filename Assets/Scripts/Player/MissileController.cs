using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private bool isBullet;
    [SerializeField] private GameObject explosionParticlePrefab;
    [SerializeField] private int particleSpawnCount = 10;

    public Vector3 fireDestination;
    public float speed = 10f;
    public float damage = 1f;
    public float lifetime = 5f;

    public float explosionRadius;
    [SerializeField] private LayerMask enemyLayer; // optional but recommended

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (!isBullet)
        {
            transform.position = Vector3.MoveTowards(
              transform.position,
              fireDestination,
              speed * Time.deltaTime
           );

            // Check if we've arrived
            if (Vector3.Distance(transform.position, fireDestination) < 0.05f)
            {
                Explode();
            }
        } else
        {
            // Bullet logic (straight line in forward direction)
            transform.position += transform.up * speed * Time.deltaTime; // new // use .right or .up depending on sprite

            // Optional: destroy bullet after a certain distance or lifetime
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                Destroy(gameObject);
            }
        }
       
    }

    void Explode()
    {
        Vector2 center = transform.position;

        // Damage logic
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            center,
            explosionRadius,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // Spawn particles randomly in radius
        for (int i = 0; i < particleSpawnCount; i++)
        {
            Vector2 randomPoint = center + Random.insideUnitCircle * explosionRadius;

            Instantiate(
                explosionParticlePrefab,
                randomPoint,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }

    //Only for machine gun
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy") && isBullet)
        {

            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
