using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animationBoolName) : base(enemy, enemyStateMachine, animationBoolName)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();

        MoveEnter();

        if (_enemy.CheckForAttack())
        {
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();

        MoveExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

}
