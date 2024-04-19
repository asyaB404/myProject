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
        // enemy.rb.MovePosition(
        //     (Vector2)enemy.transform.position
        //         + enemy.DirectionToPlayer
        //             * enemy.info.speed
        //             * GameData.GlobalMoveSpeed
        //             * Time.deltaTime
        // );
        enemy.rb.velocity = enemy.DirectionToPlayer * enemy.info.speed * GameData.GlobalMoveSpeed;
    }
}
