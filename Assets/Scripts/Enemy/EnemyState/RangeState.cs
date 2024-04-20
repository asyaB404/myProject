using DG.Tweening;
using UnityEngine;

public class RangeState : EnemyState
{
    private float shootCD = 0.5f;

    public RangeState(EnemyStateMachine stateMachine, EnemyBase enemy, float stateTimer = 0)
        : base(stateMachine, enemy, stateTimer)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
        this.stateTimer = stateTimer;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float len = (
            PlayerController.Instance.transform.position - enemy.transform.position
        ).magnitude;
        EnemyInfo info = enemy.info;
        if (len < info.range * GameData.GlobalRange)
        {
            shootCD -= Time.deltaTime * info.atkSpeed / 100;
            if (shootCD <= 0)
            {
                shootCD = 1;
                enemy
                    .transform.DOScale(Vector3.one * 1.25f, 0.2f)
                    // 缩放变大完成后执行缩放恢复动画
                    .OnComplete(
                        () =>
                            enemy
                                .transform.DOScale(Vector3.one, 0.2f)
                                .SetEase(Ease.OutBounce)
                                .OnComplete(() =>
                                {
                                    GameObject bullet = GameObject.Instantiate(
                                        (enemy as Enemy2).bullet
                                    );
                                    bullet.transform.position = enemy.transform.position;
                                    bullet
                                        .GetComponent<EnemyBullet>()
                                        .Init(enemy.DirectionToPlayer, info);
                                })
                    );
            }
        }
        else
        {
            stateMachine.ChangeState(new CloseState(stateMachine, enemy));
        }
    }
}
