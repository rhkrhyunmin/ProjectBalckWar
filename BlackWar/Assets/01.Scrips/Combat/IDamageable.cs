using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ArmyApplyDamage(int damage);

    public void EnemyApplyDamage(int damage);
}
