using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public EnemyState PrevState { get; private set; }
    public Dictionary<EnemyStateEnum, EnemyState> StateDictionary { get; private set; } = new();

    public void Init(EnemyStateEnum state)
    {
        CurrentState = StateDictionary[state];
        CurrentState.OnEnterState();
    }

    public void ChangeState(EnemyStateEnum newState)
    {
        CurrentState.OnExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.OnEnterState();
    }

    public void AddState(EnemyStateEnum stateType, EnemyState enemyState)
    {
        StateDictionary.Add(stateType, enemyState);
    }
}
