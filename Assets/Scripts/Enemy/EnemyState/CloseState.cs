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
        enemy.rb.velocity = enemy.DirectionToPlayer * enemy.info.speed * GameData.GlobalMoveSpeed;
        float len = (
            PlayerController.Instance.transform.position - enemy.transform.position
        ).magnitude;
        EnemyInfo info = enemy.info;
        if (len < info.range * GameData.GlobalRange)
        {
            if (info.enemyType == 1)
            {
                stateMachine.ChangeState(new RushState(stateMachine, enemy));
            }
            else if (info.enemyType == 2)
            {
                stateMachine.ChangeState(new RangeState(stateMachine, enemy));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.rb.velocity = Vector2.zero;
    }
}
