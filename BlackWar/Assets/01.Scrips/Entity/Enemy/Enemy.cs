using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] protected EnemyStat _enemyStat;
    public LayerMask armyLayer;

    public EnemyStat Stat
    {
        get => _enemyStat;
        set => _enemyStat = value;
    }

    public Animator AnimatorCompo { get; protected set; }
    public EnemyStateMachine StateMachine { get; private set; }

    public virtual void StateInit() { }

    public virtual void AnimationTrigger() { }

    protected void SetBaseState()
    {
        StateMachine = new EnemyStateMachine();

        foreach (EnemyStateEnum state in Enum.GetValues(typeof(EnemyStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Enemy{typeName}State");
            try
            {
                EnemyState newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState;

                if (newState == null)
                {
                    Debug.LogError($"There is no script : {state}");
                    return;
                }
                StateMachine.AddState(state, newState);
            }
            catch
            {

            }
        }
    }

    #region ¿òÁ÷ÀÓ
    public void MoveEnemy()
    {
        transform.Translate(Vector2.left * _enemyStat.MoveSpeed.GetValue() * Time.deltaTime);
    }

    public bool CheckForAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _enemyStat.AttackDistance.GetValue(), armyLayer);

        if (enemies.Length > 0)
            return true;
        else
            return false;
    }

    #endregion
}
