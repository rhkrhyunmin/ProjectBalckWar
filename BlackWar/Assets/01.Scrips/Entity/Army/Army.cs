using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Army : Entity
{
    public PlayerStat _armyStat;
    [HideInInspector]
    public Collider2D _col;
    [HideInInspector]
    public Enemy enemy;

    //public WeaponType weaponType;

    public PlayerStat Stat
    {
        get => _armyStat;
        set => _armyStat = value;
    }

    #region Components
    public ArmyEntityAttackData AttackCompo { get; private set; }
    public ArmyStateMachine StateMachine { get; private set; }
    #endregion
    public GameObject _weapon;

    [HideInInspector]
    public Collider2D _enemy;

    protected virtual void Awake()
    {
        base.Awake();
        AttackCompo = GetComponent<ArmyEntityAttackData>();
        SetBaseState();
    }

    protected override void Start()
    {
        currentHp = _armyStat.MaxHp.GetValue();
        _col = GetComponent<Collider2D>();
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
        transform.Translate(Vector2.right * _armyStat.MoveSpeed.GetValue() * Time.deltaTime);
    }

    public bool CheckForAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _armyStat.AttackDistance.GetValue(), enemyLayer);
        List<Enemy> enemies = new List<Enemy>();

        foreach (Collider2D collider in colliders)
        {
            enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
                _enemy = collider;
            }
        }

        if (enemies.Count > 0)
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
