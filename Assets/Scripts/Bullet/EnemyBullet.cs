using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 12f;
    private float timer = 0;
    public EnemyInfo info;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats.IsInv)
            {
                if (info.energyType == EnergyType.Anode)
                    playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
                else
                    playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
                playerStats.TakeDamage(Mathf.FloorToInt(info.atkMul));
            }
            DoDestroy();
        }
    }

    public void Init(Vector2 diretion, EnemyInfo info)
    {
        this.info = info;
        rb.velocity = diretion * speed;
    }

    private void Update()
    {
        if (timer < info.bulletRange * GameData.GlobalBulletFlyTime)
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
