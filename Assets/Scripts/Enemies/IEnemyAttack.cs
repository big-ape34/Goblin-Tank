using UnityEngine;

public interface IEnemyAttack
{
    float Damage { get; }
    void InitAttack(Transform target);
}
