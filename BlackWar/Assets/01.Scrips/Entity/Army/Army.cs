using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}

public class Army : PoolableMono
{
    public PlayerStat _armyStat;
    public LayerMask enemyLayer;

    public WeaponType weaponType;

    public PlayerStat Stat
    {
        get => _armyStat;
        set => _armyStat = value;
    }

    public Animator AnimatorCompo { get; protected set; }
    public ArmyEntityAttackData AttackCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; protected set; }
    public ArmyStateMachine StateMachine { get; private set; }

    public float _currentHp;
    public GameObject _weapon;

    protected virtual void Awake()
    {
        AnimatorCompo = GetComponentInChildren<Animator>();
        DamageCasterCompo = GetComponentInChildren<DamageCaster>();
        AttackCompo = GetComponent<ArmyEntityAttackData>();
    }

    protected virtual void Start()
    {
        _currentHp = _armyStat.MaxHp.GetValue();
    }

    protected virtual void Update()
    {
        
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

    #region 움직임
    public void MoveArmy()
    {
        transform.Translate(Vector2.right * _armyStat.MoveSpeed.GetValue() * Time.deltaTime);
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

    #region 공격
    public void Attack()
    {
        /*if (weaponType == WeaponType.Melee)
        {
            AttackCompo.MeleeAttack(_armyStat.AttackPower.GetValue());
        }

        else if (weaponType == WeaponType.Ranged)
        {
            AttackCompo.RangerAttack(_armyStat.AttackPower.GetValue());
        }*/
    }

    #endregion
}
