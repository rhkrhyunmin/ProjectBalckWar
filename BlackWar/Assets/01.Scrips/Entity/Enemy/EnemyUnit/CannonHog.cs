using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonHogStateEnum
{
    Move,
    Attack
}

public class CannonHog : Enemy
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
        StateMachine.Init(EnemyStateEnum.Move);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.OnUpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
