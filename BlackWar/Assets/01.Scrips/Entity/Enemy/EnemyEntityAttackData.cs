using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityAttackData : MonoBehaviour
{
    protected Enemy enemy;
    DamageCaster DamageCasterCompo => enemy.DamageCasterCompo;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public virtual void MeleeAttack()
    {

    }
}
