using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;

    protected Transform tankTransform;

    protected virtual void Start()
    {
        GameObject tank = GameObject.FindGameObjectWithTag("Player");
        if (tank != null)
            tankTransform = tank.transform;
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
