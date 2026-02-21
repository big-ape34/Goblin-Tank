using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public static event System.Action OnEnemyKilled;

    public float moveSpeed = 3f;
    public float maxHealth = 1f;

    [HideInInspector] public bool isAttacking = false;

    protected Transform tankTransform;
    private float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        GameObject tank = GameObject.FindGameObjectWithTag("Tank");
        if (tank != null)
            tankTransform = tank.transform;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }

    const float WorldHalfSize = 100f;

    void Update()
    {
        if (tankTransform != null && !isAttacking)
            Move();

        // Clamp within world bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -WorldHalfSize, WorldHalfSize);
        pos.y = Mathf.Clamp(pos.y, -WorldHalfSize, WorldHalfSize);
        transform.position = pos;
    }

    protected abstract void Move();

    protected void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
