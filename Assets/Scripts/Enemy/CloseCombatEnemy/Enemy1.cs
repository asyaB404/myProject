/// <summary>
/// 基础近战怪物,只能向玩家贴近 碰撞到玩家造成伤害
/// </summary>
public class Enemy1 : EnemyBase
{
    public override void Start()
    {
        base.Start();
        stateMachine.CreateState(new CloseState(stateMachine, this));
    }
}
