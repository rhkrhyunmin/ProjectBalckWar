using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMoveState : ArmyState
{
    public ArmyMoveState(Army army, ArmyStateMachine armyStateMachine, string animationBoolName) : base(army, armyStateMachine, animationBoolName)
    {
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        MoveEnter();
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
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
