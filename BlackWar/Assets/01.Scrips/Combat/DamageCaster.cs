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
    private float _detectRange = 1f;
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
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        int damage = (int)_enemy.Stat.AttackPower.GetValue();
        Debug.Log($"Enemy Damage = {damage}");

        foreach(var col in Colls)
        {
            _target = col.GetComponentInParent<Entity>();
            _target.currentHp -= damage;
            Debug.Log(_target.currentHp);
        }
    }

    public void ArmyCastDamage()
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        int damage = (int)_army.Stat.AttackPower.GetValue();
        Debug.Log($"Army Damage = {damage}");

        foreach (var col in Colls)
        {
            _target = col.GetComponentInParent<Entity>();
            _target.currentHp -= damage;
        }
    }
}
