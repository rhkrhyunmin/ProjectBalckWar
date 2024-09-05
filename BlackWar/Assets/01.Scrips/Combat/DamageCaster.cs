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

    //공격들 구현
    public void EnemyCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);

        int damage = (int)_enemy.Stat.AttackPower.GetValue();

        if (colliders.Length == 0)
            return;
        else
        {
            foreach (var col in colliders)
            {
                _target = col.GetComponentInParent<Army>();
                _target.currentHp -= damage;
            }
        }
    }

    public void ArmyCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);
        int damage = (int)_army.Stat.AttackPower.GetValue();

        if (colliders.Length == 0)
            return;
        else
        {
            foreach (var col in colliders)
            {
                _target = col.GetComponentInParent<Enemy>();
                _target.currentHp -= damage;
            }
        }
    }
}
