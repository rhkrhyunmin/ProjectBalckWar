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

    #region Enter
    protected void MoveEnter()
    {
        _triggerCalled = true;
        _army.MoveArmy();
    }

    protected void AttackEnter()
    {
        _triggerCalled = false;
    }
    #endregion

    #region Exit
    protected void MoveExit()
    {

    }

    protected void AttackExit()
    {
        _army.AnimatorCompo.speed = 1;
    }
    #endregion

    public virtual void OnEnterState()
    {
        _army.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ
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
