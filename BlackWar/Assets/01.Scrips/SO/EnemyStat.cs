using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EnemyStatType
{
    MaxHp,
    MoveSpeed,
    AttackPower,
}

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class EnemyStat : ScriptableObject
{
    private Enemy _owner;

    private Dictionary<EnemyStatType, FieldInfo> _fieldInfoDictionary = new Dictionary<EnemyStatType, FieldInfo>();

    [Header("Situation")]
    public Stat MaxHp;
    public Stat MoveSpeed;

    [Header("Attack")]
    public Stat AttackPower;
    public Stat AttackTimer;
    public Stat AttackDistance;
    public Stat AttackDelay;

    private void OnEnable()
    {
        Type enemyStatType = GetType();

        foreach (EnemyStatType statType in Enum.GetValues(typeof(EnemyStatType)))
        {
            string statName = statType.ToString();
            FieldInfo statField = enemyStatType.GetField(statName);

            if (statField == null)
            {
                Debug.LogError($"{statName} stat doesn't exist.");
            }
            else
            {
                _fieldInfoDictionary.Add(statType, statField);
            }
        }
    }

    public void SetOwner(Enemy owner)
    {
        _owner = owner;
    }

    public void InitializeAllModifiers()
    {
        foreach (EnemyStatType statType in Enum.GetValues(typeof(EnemyStatType)))
        {
            GetStatByType(statType).InitializeModifier();
        }
    }

    public Stat GetStatByType(EnemyStatType statType)
    {
        return _fieldInfoDictionary[statType].GetValue(this) as Stat;
    }
}
