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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = -1;
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats.IsInv)
            {
                if (info.energyType == EnergyType.Anode)
                {
                    playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
                    animator.SetTrigger("1");
                }
                else
                {
                    playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
                    animator.SetTrigger("0");
                }
                rb.velocity = Vector2.zero;
                playerStats.TakeDamage(Mathf.FloorToInt(info.atkMul));
            }
            // DoDestroy();
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
            timer < info.bulletRange * GameData.GlobalBulletFlyTime
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
        Destroy(gameObject);
    }
}
