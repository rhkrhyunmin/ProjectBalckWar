using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public LayerMask castleTargetLayer;

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

    //아군이 공격
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

    //적이 공격
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
            Debug.Log("데미지");
            //int damage = (int)_enemy.Stat.AttackPower.GetValue();
            int damage = 10;
            damageable.ArmyApplyDamage(damage);
        }
    }

    public void ArmyRangeCastDamage()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, TargetLayer);

        if (colliders.Length == 0)
            return;

        IDamageable damageable = colliders[0].GetComponent<IDamageable>();
        if (damageable != null)
        {
            int damage = (int)_army.Stat.AttackPower.GetValue();
            damageable.EnemyApplyDamage(damage);
        }
    }

    //성 공격

    public void ArmyCastleCastDamage()
    {
            var _castleColliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, _army.castleLayer);

            if (_castleColliders.Length == 0)
                return;
            else
            {
                foreach (var collider in _castleColliders)
                {
                    IDamageable damageable = collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        int damage = (int)_army.Stat.AttackPower.GetValue();
                        damageable.EnemyCastleApplyDamage(damage);
                    }
                }
            }
        
    }

    public void EnemyCastleCastDamage()
    {
            var _castleColliders = Physics2D.OverlapCircleAll(transform.position, _detectRange, _enemy.castleLayer);

            if (_castleColliders.Length == 0)
                return;
            else
            {
                foreach (var collider in _castleColliders)
                {
                    IDamageable damageable = collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        int damage = (int)_enemy.Stat.AttackPower.GetValue();
                        damageable.ArmyCastleApplyDamage(damage);
                    }
                }
            }
        }
    
}
