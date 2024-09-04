using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : RangedAttack
{
    private GameObject Spawn;
    Army _army;

    private float timer = 0;

    public void Start()
    {
        _army = GetComponent<Army>();
        Spawn = _army._weapon;
    }

    public void Update()
    {
        AttackBow();
    }

    public void AttackBow()
    {
        timer += Time.deltaTime;

        if (_army.CheckForAttack() && timer > _army._armyStat.AttackDelay.GetValue())
        {
            //StartShooting(PoolManager.Instance.Pop(PoolType.Arrow,Spawn.transform.position),Spawn.transform.position, _army._enemy.transform.position, 5);
            timer = 0;
        }
    }

}
