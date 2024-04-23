using UnityEngine;

/// <summary>
/// 基础远程怪物:只能向玩家贴近 进入射程后对玩家当前方向发射1子弹
/// </summary>
public class Enemy2 : EnemyBase
{
    public GameObject bullet;
    public float Shootduration = 0;

    public override void Start()
    {
        base.Start();
        stateMachine.CreateState(new CloseState(stateMachine, this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, info.range * GameData.GlobalRange);
    }
}
