using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] protected PlayerStat _armyStat;
    public LayerMask enemyLayer;

    public PlayerStat _arkyStat
    {
        get => _armyStat;
        set => _armyStat = value;
    }

    public Animator AnimatorCompo { get; protected set; } //나중에 Entity를 만들어 따로 옮길 것입니다.
    public ArmyStateMachine StateMachine { get; private set; }

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
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
}
