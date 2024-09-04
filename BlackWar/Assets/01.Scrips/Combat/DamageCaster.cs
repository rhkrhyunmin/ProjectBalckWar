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

    Army army;
    Enemy enemy;

    private void Start()
    {
        army = GetComponent<Army>();
        enemy = GetComponent<Enemy>();  
    }

    public void SetPostion()
    {
        transform.localPosition = Vector3.zero;
    }

    //공격들 구현

    //기본공격
   /* private void OnTriggerEnter(Collider other)
    {
        // Assuming 'weapon' is tagged as "Weapon"
        if (other.CompareTag("Weapon"))
        {
            CastDamage();
        }
    }*/

    public void CastDamage()
    {
        if (Origin.Army == orginType)
            enemy.OnHit(enemy._enemyStat.AttackPower.GetValue());
        else if (Origin.Enemy == orginType)
            army.OnHit(army._armyStat.AttackPower.GetValue());
    }
}
