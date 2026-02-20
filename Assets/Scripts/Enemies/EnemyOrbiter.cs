using UnityEngine;

public class EnemyOrbiter : EnemyBase
{
    public float orbitRadius;
    public float orbitSpeed;

    float currentAngle;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;

        if (tankTransform != null)
        {
            Vector3 offset = transform.position - tankTransform.position;
            currentAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        }
    }

    protected override void Move()
    {
        currentAngle += orbitSpeed * Time.deltaTime;

        Vector3 orbitPosition = tankTransform.position + new Vector3(
            Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius,
            Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius,
            0f
        );

        transform.position = Vector3.Lerp(
            transform.position,
            orbitPosition,
            moveSpeed * Time.deltaTime
        );

        FaceTarget(tankTransform.position);
    }
}
