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
        EnemyInfo info = enemy.info;
        float rng = info.range * GameData.GlobalRange;
        float rangeSqr = rng * rng;

        if (enemy.SqrDistanceToPlayer < rangeSqr)
        {
            shootCD += Time.deltaTime * info.atkSpeed / 100;
            if (shootCD >= 1)
            {
                Vector3 scale = enemy.transform.localScale;
                enemy.anim.SetTrigger("fire");
                shootCD = 0 - (enemy as Enemy2).Shootduration;
                enemy
                    .transform.DOScale(scale * 1.25f, 0.2f)
                    .OnComplete(
                        () =>
                            enemy
                                .transform.DOScale(scale, 0.2f)
                                .SetEase(Ease.OutBounce)
                                .OnComplete(() =>
                                {
                                    Enemy2 shooter = enemy as Enemy2;
                                    if (shooter != null && shooter.bullet != null)
                                    {
                                        GameObject b = ProjectilePools.AcquireEnemyBullet(
                                            shooter.bullet,
                                            enemy.transform.position,
                                            Quaternion.identity
                                        );
                                        b.GetComponent<IEnemyBullet>()
                                            .Init(enemy.GetDirectionToPlayerNow(), info);
                                    }
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
