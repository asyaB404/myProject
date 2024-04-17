using UnityEngine;

public class CloseState : EnemyState
{
    public CloseState(EnemyStateMachine stateMachine, EnemyBase enemy, float stateTimer = 0)
        : base(stateMachine, enemy, stateTimer)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        enemy.transform.Translate(enemy.DirectionToPlayer * enemy.info.speed * Time.deltaTime);
    }
}
