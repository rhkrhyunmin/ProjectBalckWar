using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class ArmyEntityAttackData : RangedAttack
{
    
    protected Army army;
    DamageCaster DamageCasterCompo => army.DamageCasterCompo;

    private float timer = 0;

    private void Awake()
    {
        army = GetComponent<Army>();
    }

    public virtual void RangerAttack()
    {
        timer += Time.deltaTime;
        if (army.poolType == PoolType.Archer)
        {
            if (timer > army.Stat.AttackDelay.GetValue())
            {
                StartCoroutine(ShootProjectile(army._weapon.transform, PoolManager.Instance.Pop(PoolType.Arrow, army._weapon.transform.transform.position), army._enemy.transform, 0, 7, true));
                Debug.Log("123");
                timer = 0;
            }
        }

        else if(army.poolType == PoolType.Ninja)
        {

        }
    }

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.ArmyCastDamage();
    }
}
