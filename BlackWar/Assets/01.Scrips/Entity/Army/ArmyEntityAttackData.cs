using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum AttackType
{
    ShortRange,
    LongRange
}


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

    public virtual void OnTriggerEnter2D(Collider2D other)
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
    }

    public void AttackBow()
    {
        timer += Time.deltaTime;

        if (army.CheckForAttack() && timer > army._armyStat.AttackDelay.GetValue() && AttackType == AttackType.LongRange)
        {
            StartCoroutine(ShootProjectile(army._weapon.transform, PoolManager.Instance.Pop(PoolType.Arrow, army._weapon.transform.position), army._enemy.transform, 0, 10));
            timer = 0;
        }
    }

    public virtual void RangerAttack()
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
    }

    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) != 0);
    }
}
