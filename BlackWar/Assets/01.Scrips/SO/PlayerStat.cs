using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class PlayerStat : ScriptableObject
{
    [Header("Situation")]
    public Stat MaxHp;
    public Stat MoveSpeed;
    public Stat AttackPower;

    [Header("UseCost")]
    public Stat UseCost;
}
