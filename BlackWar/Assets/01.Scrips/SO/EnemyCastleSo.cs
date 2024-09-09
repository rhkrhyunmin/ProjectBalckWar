using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/EnemyCastle")]
public class EnemyCastleSo : ScriptableObject
{
    private EnemyCastleControl _owner;

    [Header("Situation")]
    public Stat MaxHp;

    [Header("Attack")]
    public Stat AttackPower;
    public Stat AttackTimer;

    [Header("Angle")]
    public Stat angle;
}
