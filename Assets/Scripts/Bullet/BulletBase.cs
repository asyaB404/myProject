using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    private int penetrableCount = 1;
    private EnergyType type;
    private float damage;
    private float duration = 6f;

    [SerializeField]
    private float speed = 12f;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBase>() != null && penetrableCount > 0)
        {
            penetrableCount--;
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            if (enemy.info.energyType == type)
            {
                PlayerStats playerStats = PlayerController.Instance.playerStats;
                if (playerStats.Critical.GetValue() > Random.Range(0f, 1f))
                {
                    damage *= playerStats.criticalStrikeMultiplier.GetValue();
                }
                enemy.TakeDamage(damage);
            }
            if (penetrableCount <= 0)
                Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }

    public void SetupBullet(
        int _penetrableCount,
        EnergyType _type,
        float _damage,
        float _speed = 12f
    )
    {
        this.damage = _damage;
        this.type = _type;
        this.penetrableCount = _penetrableCount;
        this.speed = _speed;
    }
}
