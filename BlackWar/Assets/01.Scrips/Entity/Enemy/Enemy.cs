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

    public bool IsDead
    {
        get
        {
            if (HealthCompo != null)
                return HealthCompo.IsDead;
            return false;
        }
        set
        {
            HealthCompo.IsDead = value;
        }
    }

    #region Components
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyEntityAttackData AttackCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        AttackCompo = GetComponent<EnemyEntityAttackData>();
        HealthCompo = GetComponent<Health>();
        SetBaseState();

        HealthCompo?.EnemySetHealth(EnemyStat);
        EnemyStat = Instantiate(EnemyStat);
    }

    protected override void Start()
    {
        base.Start();
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

    #region 움직임
    public void MoveEnemy()
    {
        transform.Translate(Vector2.left * EnemyStat.MoveSpeed.GetValue() * Time.deltaTime);
    }

    public bool CheckForAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, EnemyStat.AttackDistance.GetValue(), enemyLayer);
        Collider2D castle = Physics2D.OverlapCircle(transform.position, EnemyStat.AttackDistance.GetValue(), castleLayer);

        if (enemies.Length > 0 || castle != null)  // 적이 있거나 성이 있을 때 true 반환
            return true;
        else
            return false;
    }


    #endregion
}
