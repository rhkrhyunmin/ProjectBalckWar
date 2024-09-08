using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Castle")]
public class CastleSO : ScriptableObject
{
    private CastleControl _owner;

    [Header("Situation")]
    public Stat MaxHp;

    [Header("Attack")]
    public Stat AttackPower;
    public Stat AttackTimer;

    [Header("Angle")]
    public Stat angle;
}
