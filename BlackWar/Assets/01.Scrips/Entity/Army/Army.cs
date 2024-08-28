using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : Entity
{
    [SerializeField] protected PlayerStat _armyStat;

    public PlayerStat Stat
    {
        get => _armyStat;
        set => _armyStat = value;
    }

    #region Components
    public ArmyEntityAttackData AttackCompo { get; private set; }
    public ArmyStateMachine StateMachine { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        AttackCompo = GetComponent<ArmyEntityAttackData>();
        SetBaseState();
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
        StateMachine = new ArmyStateMachine();

        foreach (ArmyStateEnum state in Enum.GetValues(typeof(ArmyStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Army{typeName}State");
            try
            {
                ArmyState newState = Activator.CreateInstance(t, this, StateMachine, typeName) as ArmyState;

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
    public void MoveArmy()
    {
        transform.Translate(Vector2.right * _armyStat.MoveSpeed.GetValue() * Time.deltaTime);-
    }

    public bool CheckForAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _armyStat.AttackDistance.GetValue(), enemyLayer);

        if(enemies.Length > 0)
            return true;
        else
            return false;
    }

    #endregion
}
