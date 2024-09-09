using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    protected Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    public void AttackTrigger()
    {
        _enemy.AttackCompo.MeleeAttack();
    }

    public void ShotCannonTrigger()
    {
        _enemy.AttackCompo.CanonRangeAttack();
    }

    public void DeadTrigger()
    {
        PoolManager.Instance.Push(_enemy);
    }
}
