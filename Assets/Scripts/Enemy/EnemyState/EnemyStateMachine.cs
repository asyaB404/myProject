using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurState { get; private set; }
    public void CreateState(EnemyState newState)
    {
        CurState = newState;
        CurState.OnEnter();
    }
    public void ChangeState(EnemyState newState)
    {
        CurState.OnExit();
        CurState = newState;
        CurState.OnEnter();
    }
}
