using UnityEngine;

public class EnemyFlanker : EnemyBase
{
    public float avoidAngle = 120f;

    private TurretController turretController;

    protected override void Start()
    {
        moveSpeed = 4f;
        maxHealth = 3f;
        base.Start();
        turretController = FindObjectOfType<TurretController>();
    }

    protected override void Move()
    {
        if (turretController == null)
        {
            MoveDirectlyToTank();
            return;
        }

        Vector2 aimDir = turretController.AimDirection;
        Vector2 toFlanker = ((Vector2)transform.position - (Vector2)tankTransform.position);
        float angle = Vector2.SignedAngle(aimDir, toFlanker);
        float halfCone = avoidAngle / 2f;

        if (Mathf.Abs(angle) < halfCone)
        {
            // Inside the danger cone — escape to the nearest edge
            float escapeAngle = angle >= 0 ? halfCone + 15f : -(halfCone + 15f);
            Vector2 escapeDir = RotateVector(aimDir, escapeAngle).normalized;
            Vector3 escapeTarget = tankTransform.position + (Vector3)(escapeDir * toFlanker.magnitude);

            Vector3 direction = (escapeTarget - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            FaceTarget(escapeTarget);
        }
        else
        {
            MoveDirectlyToTank();
        }
    }

    private void MoveDirectlyToTank()
    {
        Vector3 direction = (tankTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        FaceTarget(tankTransform.position);
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}
