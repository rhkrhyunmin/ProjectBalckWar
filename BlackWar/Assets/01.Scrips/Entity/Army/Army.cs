using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GetType
{
    Friend,
    Enemy,
}

public class Army : Entity
{
    public PlayerStat _armyStat;
    [HideInInspector]
    public Collider2D _col;

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

    public float _currentHp;
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
        _currentHp = _armyStat.MaxHp.GetValue();
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _armyStat.AttackDistance.GetValue(), enemyLayer);
        if(enemies.Length > 0)
        {
            foreach(Collider2D enemy in enemies)
            {
                _enemy = enemy;
            }
            Debug.Log("1");
            return true;
        }
        else
            return false;
    }

    #endregion

    public void OnHit(float damager)
    {   
        _currentHp -= damager;
        Debug.Log("55");

        if(_currentHp < 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        //PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
