using UnityEngine;

public class EnemyCharger : EnemyBase
{
    protected override void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            tankTransform.position,
            moveSpeed * Time.deltaTime
        );
        FaceTarget(tankTransform.position);
    }
}
