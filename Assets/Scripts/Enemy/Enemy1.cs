/// <summary>
/// 近战怪物,只能向玩家贴近 碰撞到玩家造成伤害 如果info里距离不为零可以进行冲刺
/// </summary>
public class Enemy1 : EnemyBase
{
    /// <summary>
    /// 冲刺间隔
    /// </summary>
    public float rushDuration = 1;
    public override void Start()
    {
        base.Start();
        stateMachine.CreateState(new CloseState(stateMachine, this));
    }
}
