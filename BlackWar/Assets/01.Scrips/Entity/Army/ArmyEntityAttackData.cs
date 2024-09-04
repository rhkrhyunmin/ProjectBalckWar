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

    /*public virtual void OnTriggerEnter2D(Collider2D other)
    {
        timer += Time.deltaTime;
        if (other.CompareTag("Weapon"))
        {
            Army weaponArmy = other.GetComponentInParent<Army>();

            if (AttackType == AttackType.ShortRange && army._armyStat.AttackTimer.GetValue() > timer)
            {
                float attackPower = weaponArmy._armyStat.AttackPower.GetValue();
                army.OnHit(attackPower);
                timer = 0;
            }
        }
    }*/


    public virtual void RangerAttack()
    {

    }

    public virtual void MeleeAttack()
    {
        DamageCasterCompo.CastDamage();
    }

    /*public virtual void RangerAttack(float damage)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, army._armyStat.AttackDistance.GetValue(), army.enemyLayer);
        Debug.Log("123");
        if (hit.collider != null)
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (IsInLayerMask(hitLayer, army.enemyLayer))
            {
                AttackBow();
            }
        }
    }*/

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
