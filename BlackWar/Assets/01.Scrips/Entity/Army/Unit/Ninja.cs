using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Army
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        StateInit();
    }

    public override void StateInit()
    {
        StateMachine.Init(ArmyStateEnum.Move);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.OnUpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
