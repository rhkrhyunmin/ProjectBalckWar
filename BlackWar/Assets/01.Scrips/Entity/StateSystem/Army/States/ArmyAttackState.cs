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

        if(_triggerCalled) //공격
        {
            //코드 작성
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
