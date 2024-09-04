using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class ArmyEntityAttackData : RangedAttack
{
    public AttackType AttackType;
    protected Army army;
    DamageCaster DamageCasterCompo => army.DamageCasterCompo;

    private float timer = 0;

    private void Awake()
    {
        army = GetComponent<Army>();
    }

    public virtual void RangerAttack()
    {

    }

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
