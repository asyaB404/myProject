using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState curState { get; private set; }
    public void CreateState(EnemyState newState)
    {
        curState = newState;
        curState.OnEnter();
    }
    public void ChangeState(EnemyState newState)
    {
        curState.OnExit();
        curState = newState;
        curState.OnEnter();
    }
}
