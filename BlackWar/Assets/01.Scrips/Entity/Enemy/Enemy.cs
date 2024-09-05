using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStat EnemyStat;

    public EnemyStat Stat
    {
        get => EnemyStat;
        set => EnemyStat = value;
    }

    #region Components
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyEntityAttackData AttackCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        AttackCompo = GetComponent<EnemyEntityAttackData>();
        SetBaseState();
    }

    protected override void Start()
    {
        base.Start();
        currentHp = EnemyStat.MaxHp.GetValue();
    }

    protected override void Update()
    {
        base.Update();
    }

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

    #region ������
    public void MoveEnemy()
    {
        transform.Translate(Vector2.left * EnemyStat.MoveSpeed.GetValue() * Time.deltaTime);
    }

    public bool CheckForAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, EnemyStat.AttackDistance.GetValue(), enemyLayer);

        if (enemies.Length > 0)
            return true;
        else
            return false;
    }

    #endregion
}
