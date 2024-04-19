using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private Dictionary<string, EnemyState> dict = new();
    public EnemyState CurState { get; private set; }

    public void CreateState(EnemyState newState)
    {
        dict.Add(newState.GetType().Name, newState);
        CurState = newState;
        CurState.OnEnter();
    }

    public void ChangeState(EnemyState newState)
    {
        CurState.OnExit();
        if (dict.TryGetValue(newState.GetType().Name, out EnemyState state))
        {
            CurState = state;
            CurState.OnEnter();
        }
        else
        {
            CreateState(newState);
        }
    }
}
