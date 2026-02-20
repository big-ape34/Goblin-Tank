using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int maxHealth = 1;

    protected Transform tankTransform;
    private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        GameObject tank = GameObject.FindGameObjectWithTag("Tank");
        if (tank != null)
            tankTransform = tank.transform;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (tankTransform != null)
            Move();
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
