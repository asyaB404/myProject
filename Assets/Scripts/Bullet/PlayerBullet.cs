using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sr;
    private int penetrableCount = 1;
    private EnergyType type;
    private float damage;
    private float duration = 6f;
    public Sprite sprite1;
    public Sprite sprite2;

    [SerializeField]
    private float speed = 12f;
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemyBase = collision.GetComponent<EnemyBase>();
        if (enemyBase != null && !enemyBase.isDie && penetrableCount > 0)
        {
            penetrableCount--;
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            if (enemy.info.energyType != type)
            {
                PlayerStats playerStats = PlayerController.Instance.playerStats;
                Text text = WorldCanvas.Instacne.ShowMessage(
                    enemy.transform.position
                        + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)),
                    Mathf.FloorToInt(damage).ToString()
                );
                if (playerStats.Critical > Random.Range(0f, 1f))
                {
                    damage *= playerStats.CriticalStrikeMultiplier;
                    text.color = Color.yellow;
                    text.text = Mathf.FloorToInt(damage).ToString();
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
        transform.parent = LevelManager.Instance.bulletParent;
        if (type == EnergyType.Cathode)
            sr.sprite = sprite1;
        else
            sr.sprite = sprite2;
    }
}
