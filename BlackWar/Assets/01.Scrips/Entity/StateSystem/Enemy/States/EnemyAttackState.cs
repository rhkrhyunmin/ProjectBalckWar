using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName) : base(enemy, enemyStateMachine, animationBoolName)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        AttackEnter();
    }
    
    public override void OnUpdateState()
    {
        base.OnUpdateState();
    }

    public override void OnExitState()
    {
        base.OnExitState();

        AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
