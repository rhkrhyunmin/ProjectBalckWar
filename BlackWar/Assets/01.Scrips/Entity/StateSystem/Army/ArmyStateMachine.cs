using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArmyStateMachine
{
    public ArmyState CurrentState { get; private set; }
    public Dictionary<ArmyStateEnum, ArmyState> StateDictionary { get; private set; } = new();

    public void Init(ArmyStateEnum state)
    {
        CurrentState = StateDictionary[state];
        CurrentState.OnEnterState();
    }

    public void ChangeState(ArmyStateEnum newState)
    {
        CurrentState.OnExitState();
        CurrentState = StateDictionary[newState];
        CurrentState.OnEnterState();
    }

    public void AddState(ArmyStateEnum stateType, ArmyState playerState)
    {
        StateDictionary.Add(stateType, playerState);
    }
}