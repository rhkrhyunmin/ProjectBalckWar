using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArmyState
{
    protected ArmyStateMachine _stateMachine;
    protected Army _army;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public ArmyState(Army army, ArmyStateMachine stateMachine, string animationBoolName)
    {
        _animBoolHash = Animator.StringToHash(animationBoolName);

        _army = army;
        _stateMachine = stateMachine;
    }

    public virtual void OnEnterState()
    {
        _army.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��

    }

    public virtual void OnUpdateState()
    {

    }

    public virtual void OnExitState()
    {
        _army.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
}
