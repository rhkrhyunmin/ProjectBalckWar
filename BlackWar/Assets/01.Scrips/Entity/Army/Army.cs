using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : Entity
{
    public PlayerStat ArmyStat;
    public GameObject _weapon;
    [HideInInspector]
    public Enemy enemy;

    public PlayerStat Stat
    {
        get => ArmyStat;
        set => ArmyStat = value;
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
    public ArmyEntityAttackData AttackCompo { get; private set; }
    public ArmyStateMachine StateMachine { get; private set; }
    public Health HealthCompo { get; private set; }
    #endregion

    [HideInInspector]
    public Collider2D _enemy;

    protected virtual void Awake()
    {
        base.Awake();
        AttackCompo = GetComponent<ArmyEntityAttackData>();
        HealthCompo = GetComponent<Health>();
        SetBaseState();

        HealthCompo?.PlayerSetHealth(ArmyStat);
        ArmyStat = Instantiate(ArmyStat);
    }

    protected override void Start()
    {
        
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

    #region ������
    public void MoveArmy()
    {
        transform.Translate(Vector2.right * ArmyStat.MoveSpeed.GetValue() * Time.deltaTime);
    }

    public bool CheckForAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, ArmyStat.AttackDistance.GetValue(), enemyLayer);
        Collider2D castle = Physics2D.OverlapCircle(transform.position, ArmyStat.AttackDistance.GetValue(), castleLayer);

        if (enemies.Length > 0 || castle != null)
        {
            // 여기서 enemies 리스트를 사용하여 원하는 로직을 수행할 수 있습니다.
            return true;
        }
        else
        {
            return false;
        }
    }


    #endregion
}
