using System.Collections;
using UnityEngine;

public class ChargerAttack : MonoBehaviour, IEnemyAttack
{
    public static event System.Action OnFuseStarted;
    public static event System.Action OnChargerExploded;

    [SerializeField] private float damage = 30f;
    [SerializeField] private float fuseDuration = 3f;
    [SerializeField] private float explosionRadius = 2.5f;
    [SerializeField] private GameObject explosionPrefab;

    public float Damage => damage;

    private EnemyBase enemyBase;
    private bool fuseStarted;

    void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    public void InitAttack(Transform target) { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fuseStarted)
            return;

        if (!other.CompareTag("Tank"))
            return;

        fuseStarted = true;

        if (enemyBase != null)
            enemyBase.isAttacking = true;

        OnFuseStarted?.Invoke();
        StartCoroutine(FuseRoutine());
    }

    private IEnumerator FuseRoutine()
    {
        yield return new WaitForSeconds(fuseDuration);
        Explode();
    }

    private void Explode()
    {
        OnChargerExploded?.Invoke();

        // Damage anything in explosion radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Tank"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(damage);
            }
        }

        // Spawn explosion VFX
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Self-destruct through EnemyBase to register kill
        if (enemyBase != null)
            enemyBase.TakeDamage(9999f);
    }
}
