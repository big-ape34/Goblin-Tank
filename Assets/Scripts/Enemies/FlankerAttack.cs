using UnityEngine;

public class FlankerAttack : MonoBehaviour, IEnemyAttack
{
    public static event System.Action OnFlankerKamikaze;

    [SerializeField] private float damage = 25f;

    public float Damage => damage;

    private EnemyBase enemyBase;

    void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    public void InitAttack(Transform target) { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Tank"))
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(damage);

        OnFlankerKamikaze?.Invoke();

        // Self-destruct through EnemyBase to register kill
        if (enemyBase != null)
            enemyBase.TakeDamage(9999f);
    }
}
