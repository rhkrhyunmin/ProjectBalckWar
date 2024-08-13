using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum PlayerStatType
{
    MaxHp,
    MoveSpeed,
    AttackPower,
    UseCost,
}

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class PlayerStat : ScriptableObject
{
    private PlayerController _owner;

    private Dictionary<PlayerStatType, FieldInfo> _fieldInfoDictionary = new Dictionary<PlayerStatType, FieldInfo>();

    [Header("Situation")]
    public Stat MaxHp;
    public Stat MoveSpeed;

    [Header("Attack")]
    public Stat AttackPower;
    public Stat AttackTimer;
    public Stat AttackDistance;
    public Stat AttackDelay;

    [Header("UseCost")]
    public Stat UseCost;

    private void OnEnable()
    {
        Type playerStatType = GetType();

        foreach (PlayerStatType statType in Enum.GetValues(typeof(PlayerStatType)))
        {
            string statName = statType.ToString();
            FieldInfo statField = playerStatType.GetField(statName);

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

    public void SetOwner(PlayerController owner)
    {
        _owner = owner;
    }

    public void InitializeAllModifiers()
    {
        foreach (PlayerStatType statType in Enum.GetValues(typeof(PlayerStatType)))
        {
            GetStatByType(statType).InitializeModifier();
        }
    }

    public Stat GetStatByType(PlayerStatType statType)
    {
        return _fieldInfoDictionary[statType].GetValue(this) as Stat;
    }
}
