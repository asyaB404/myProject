using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public EnemyStateMachine stateMachine;
    public EnemyBase enemy;
    public float stateTimer;

    protected EnemyState(EnemyStateMachine stateMachine, EnemyBase enemy, float stateTimer = 0)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
    }

    public virtual void OnEnter() { }

    public virtual void OnUpdate()
    {
        stateTimer += Time.deltaTime;
    }

    public virtual void OnExit()
    {
        enemy.transform.localScale = Vector3.one;
        enemy.rb.velocity = Vector2.zero;
    }
}
