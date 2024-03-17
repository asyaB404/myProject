using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    public EnemyStateMachine stateMachine;
    public Enemy enemy;
    public float stateTimer;

    protected EnemyState(EnemyStateMachine stateMachine, Enemy enemy, float stateTimer)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
    }

    public virtual void OnEnter()
    {
        
    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnExit()
    {

    }
}
