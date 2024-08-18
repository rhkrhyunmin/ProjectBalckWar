using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine _stateMachine;
    protected Enemy _enemy;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animationBoolName)
    {
        _animBoolHash = Animator.StringToHash(animationBoolName);

        _enemy = enemy;
        _stateMachine = stateMachine;
    }

    public virtual void OnEnterState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��

    }

    public virtual void OnUpdateState()
    {

    }

    public virtual void OnExitState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
}
