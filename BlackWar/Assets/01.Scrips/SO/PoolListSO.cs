using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    None,
    knight,
    Spearman,
    Archer,
    Ninja,
}

[Serializable]
public class PoolObject
{
    //[ReadOnly]
    public string name;
    public PoolType type;
    public PoolableMono prefab;
    public int itemAmount;
}

[CreateAssetMenu(menuName = "SO/Data/PoolList")]
public class PoolListSO : ScriptableObject
{
    public List<PoolObject> poolList;
}
