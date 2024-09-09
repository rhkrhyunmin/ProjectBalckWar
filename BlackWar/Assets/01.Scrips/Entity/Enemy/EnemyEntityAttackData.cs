using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityAttackData : MonoBehaviour
{
    protected Enemy enemy;
    protected DamageCaster DamageCasterCompo => enemy.DamageCasterCompo;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.EnemyCastDamage();
        DamageCasterCompo.EnemyCastleCastDamage();
    }

    public virtual void CanonRangeAttack()
    {
        DamageCasterCompo.EnemyRangeCastDamage();
        DamageCasterCompo.EnemyCastleCastDamage();
    }
}
