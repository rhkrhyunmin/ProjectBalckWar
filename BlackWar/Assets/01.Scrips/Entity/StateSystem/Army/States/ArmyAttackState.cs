using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAttackState : ArmyState
{
    public ArmyAttackState(Army army, ArmyStateMachine armyStateMachine, string animationBoolName) : base(army, armyStateMachine, animationBoolName)
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

        if(!_army.CheckForAttack())
        {
            _stateMachine.ChangeState(ArmyStateEnum.Move);
        }
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
