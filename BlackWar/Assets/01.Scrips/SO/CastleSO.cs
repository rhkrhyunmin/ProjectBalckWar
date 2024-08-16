using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CastleStatType
{
    MaxHp,
    AttackPower,
}

[CreateAssetMenu(menuName = "SO/Stat/Castle")]
public class CastleSO : ScriptableObject
{
    private CastleControl _owner;

    [Header("Situation")]
    public Stat MaxHp;

    [Header("Attack")]
    public Stat AttackPower;
    public Stat AttackTimer;
    public Stat AttackDistance;
    public Stat AttackDelay;
}
