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

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.ArmyCastDamage();
    }

    public virtual void RangerAttack()
    {
        DamageCasterCompo.ArmyRangeCastDamage();
    }
}
