public class Enemy1 : EnemyBase
{
    public override void Start()
    {
        base.Start();
        stateMachine.CreateState(new CloseState(stateMachine, this));
    }
}
