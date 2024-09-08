using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    ShortRange,
    LongRange
}

public enum Origin
{
    Army,
    Enemy,
}

public class DamageCaster : MonoBehaviour
{
    public Origin orginType;
    private float _detectRange = 5f;
    public LayerMask TargetLayer;

    private Entity _target;

    private Army _army;
    private Enemy _enemy;

    private void Awake()
    {
        _army = GetComponentInParent<Army>();
        _enemy = GetComponentInParent<Enemy>();
    }

    public void SetPostion()
    {
        transform.localPosition = Vector3.zero;
    }

    //�Ʊ��� ����
    public void ArmyCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);

        if (colliders.Length == 0)
            return;
        else
        {
            foreach (var collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    int damage = (int)_army.Stat.AttackPower.GetValue();
                    damageable.EnemyApplyDamage(damage);
                }
            }
        }
    }

    //���� ����
    public void EnemyCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);

        if (colliders.Length == 0)
            return;
        else
        {
            foreach (var collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    int damage = (int)_enemy.Stat.AttackPower.GetValue();
                    damageable.ArmyApplyDamage(damage);
                }
            }
        }
    }

    public void EnemyRangeCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);

        if (colliders.Length == 0)
            return;

        IDamageable damageable = colliders[0].GetComponent<IDamageable>();
        if (damageable != null)
        {
            int damage = (int)_enemy.Stat.AttackPower.GetValue();
            damageable.ArmyApplyDamage(damage);
        }
    }
}
