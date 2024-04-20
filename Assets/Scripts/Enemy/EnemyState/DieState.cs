using UnityEngine;

public class DieState : EnemyState
{
    public DieState(EnemyStateMachine stateMachine, EnemyBase enemy, float stateTimer = 0)
        : base(stateMachine, enemy, stateTimer)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }    
}
