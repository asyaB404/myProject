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

    [SerializeField]
    private bool isSword;

    public bool IsSwordOrbit => isSword;

    public void ResetRuntimeStateBeforePool()
    {
        timer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemyBase = collision.GetComponent<EnemyBase>();
        if (enemyBase != null && !enemyBase.isDie && penetrableCount > 0)
        {
            if (isSword)
            {
                if (type == EnergyType.Anode)
                    damage = PlayerController.Instance.playerStats.PowerOfAnode * 1f;
                else
                    damage = PlayerController.Instance.playerStats.PowerOfCathode * 1f;
            }
            else
                penetrableCount--;

            EnemyBase enemy = enemyBase;
            if (enemy.info.energyType != type)
            {
                PlayerStats playerStats = PlayerController.Instance.playerStats;
                float dmgForHit = damage;
                bool isCrit = playerStats.Critical > Random.Range(0f, 1f);
                if (isCrit)
                    dmgForHit *= playerStats.CriticalStrikeMultiplier;

                Vector3 floatPos =
                    enemy.transform.position
                    + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                WorldCanvas.Instacne.TryShowEnemyHitDamage(
                    floatPos,
                    Mathf.FloorToInt(dmgForHit).ToString(),
                    isCrit,
                    enemy.GetInstanceID()
                );

                MusicMgr.Instance.PlaySound("hit", false, 3);
                enemy.TakeDamage(dmgForHit);
            }
            if (penetrableCount <= 0 && !isSword)
                ProjectilePools.ReleasePlayerBullet(gameObject);
        }
    }

    private void Update()
    {
        if (isSword)
        {
            return;
        }
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (timer >= duration)
        {
            ProjectilePools.ReleasePlayerBullet(gameObject);
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
        this.timer = 0f;
        transform.parent = LevelManager.Instance.bulletParent;
        if (type == EnergyType.Cathode)
            sr.sprite = sprite1;
        else
            sr.sprite = sprite2;
    }

    public void InitForSword(EnergyType energyType)
    {
        type = energyType;
        if (type == EnergyType.Cathode)
            sr.sprite = sprite1;
        else
            sr.sprite = sprite2;
        isSword = true;
    }
}
