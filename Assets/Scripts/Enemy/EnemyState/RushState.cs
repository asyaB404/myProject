using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RushState : EnemyState
{
    EnemyInfo info;

    public RushState(EnemyStateMachine stateMachine, EnemyBase enemy, float stateTimer = 0)
        : base(stateMachine, enemy, stateTimer)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
        info = enemy.info;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.autoFilp = false;
        Rush(enemy.DirectionToPlayer);
    }

    private void Rush(Vector2 dir)
    {
        enemy.StartCoroutine(RushCoroutine(dir));
    }

    IEnumerator RushCoroutine(Vector2 dir)
    {
        Transform transform = enemy.transform;
        transform.DOKill();
        transform
            .DOScale(Vector3.one * 1.5f, 0.25f)
            // 缩放变大完成后执行缩放恢复动画
            .OnComplete(() => transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce));
        yield return new WaitForSeconds(0.5f);
        enemy.rb.velocity = dir * enemy.info.speed * GameData.GlobalMoveSpeed * 2f;
        yield return new WaitForSeconds(info.bulletRange * GameData.GlobalBulletFlyTime);
        enemy.AutoFilp();
        stateMachine.ChangeState(new CloseState(stateMachine, enemy));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.autoFilp = true;
        enemy.transform.localScale = Vector3.one;
        enemy.rb.velocity = Vector2.zero;
    }
}
