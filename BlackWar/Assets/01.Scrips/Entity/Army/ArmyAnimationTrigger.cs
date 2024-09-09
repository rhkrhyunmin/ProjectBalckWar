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

    public void AttackTrigger()
    {
        _army.AttackCompo.MeleeAttack();
    }

    public void ArcherRangeAttackTrigger()
    {
        _army.AttackCompo.RangerAttack();
    }

    public void DeadTrigger()
    {
        Debug.Log("¾ö");
        _army.HealthCompo.Dead();
    }
}
