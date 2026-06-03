using UnityEngine;

public class CloseState : EnemyState
{
    private float rushTimer;

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
        if (rushTimer > 0)
            rushTimer -= Time.deltaTime;

        enemy.rb.velocity =
            enemy.DirectionToPlayer * enemy.EffectiveMoveSpeed * GameData.GlobalMoveSpeed;
        float rng = enemy.info.range * GameData.GlobalRange;
        float rangeSqr = rng * rng;
        EnemyInfo info = enemy.info;

        if (enemy.SqrDistanceToPlayer < rangeSqr)
        {
            if (info.enemyType == 1 && rushTimer <= 0)
            {
                rushTimer = (enemy as Enemy1).rushDuration;
                stateMachine.ChangeState(new RushState(stateMachine, enemy));
            }
            else if (info.enemyType == 2)
            {
                stateMachine.ChangeState(new RangeState(stateMachine, enemy));
            }
        }
    }
}
