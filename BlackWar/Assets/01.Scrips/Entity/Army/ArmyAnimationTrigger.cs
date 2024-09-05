using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAnimationTrigger : MonoBehaviour
{
    protected Army _army;

    private void Awake()
    {
        _army = transform.parent.GetComponent<Army>();
    }

    public void Update()
    {
        AttackTrigger();
    }

    public void AttackTrigger()
    {
        /*if(AttackType.LongRange == _army.AttackType)
        {
            _army.AttackCompo.RangerAttack();
        }
        else if(AttackType.ShortRange == _army.AttackType)
        {
            _army.AttackCompo.MeleeAttack();
        }*/
    }

    public void RangeAttackTrigger()
    {
        _army.AttackCompo.RangerAttack();
    }
}
