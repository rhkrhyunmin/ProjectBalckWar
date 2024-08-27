using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyEntityAttackData : MonoBehaviour
{
    protected Army army;
    DamageCaster DamageCasterCompo => army.DamageCasterCompo;

    private void Awake()
    {
        army = GetComponent<Army>();
    }

    public virtual void MeleeAttack()
    {

    }
}
