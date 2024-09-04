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

    Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
    }

    public void SetPostion()
    {
        transform.localPosition = Vector3.zero;
    }

    //공격들 구현

    public void CastDamage()
    {
        
    }
}
