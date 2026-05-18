using UnityEngine;

interface IEnemyBullet
{
    void Init(Vector2 diretion, EnemyInfo info);
}

public class EnemyBullet : MonoBehaviour, IEnemyBullet
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed = 12f;
    private float timer = 0;
    public EnemyInfo info;
    public Sprite sprite1;
    public Sprite sprite2;

    /// <summary>用于按 prefab 分桶回收到对象池。</summary>
    public GameObject EnemyBulletPrefabKey { get; private set; }

    public void AssignPoolPrefabKey(GameObject prefab)
    {
        EnemyBulletPrefabKey = prefab;
    }

    public void ResetRuntimeStateBeforePool()
    {
        timer = 0f;
        if (rb != null)
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = -1;
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (info.energyType == EnergyType.Anode)
            {
                if (!playerStats.IsInv)
                {
                    MusicMgr.Instance.PlaySound("atk_yang");
                    playerStats.AnodeEnergy += info.RecoverFromAtk;
                }
                animator.SetTrigger("1");
            }
            else
            {
                if (!playerStats.IsInv)
                {
                    playerStats.CathodeEnergy += info.RecoverFromAtk;
                    MusicMgr.Instance.PlaySound("atk_yin");
                }
                animator.SetTrigger("0");
            }
            rb.velocity = Vector2.zero;
            playerStats.TakeDamage(info.AtkMul);
        }
    }

    public void Init(Vector2 diretion, EnemyInfo info)
    {
        this.info = info;
        rb.velocity = diretion * speed;
        transform.parent = LevelManager.Instance.bulletParent;
        animator.SetBool("Black", info.energyType == EnergyType.Cathode);
    }

    private void Update()
    {
        if (
            timer < info.bulletRange * GameData.GlobalBulletFlyTime * 2
            && LevelManager.Instance.isStart
        )
        {
            timer += Time.deltaTime;
        }
        else
        {
            DoDestroy();
        }
    }

    public void DoDestroy()
    {
        ProjectilePools.ReleaseEnemyBullet(gameObject);
    }
}
