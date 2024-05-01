using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyLine : MonoBehaviour, IEnemyBullet
{
    [SerializeField]
    private SpriteRenderer sr;

    // public Vector2 size = new(1, 0.675f);
    public EnemyInfo info;

    [SerializeField]
    private BoxCollider2D box;

    [SerializeField]
    private Vector2 direction;
    public Sprite sprite1;
    public Sprite sprite11;
    public Sprite sprite2;
    public Sprite sprite22;

    public void Init(Vector2 diretion, EnemyInfo info)
    {
        this.info = info;
        float angle = Vector2.SignedAngle(Vector2.right, diretion);
        transform.Rotate(new(0, 0, angle));
        if (info.energyType == EnergyType.Cathode)
            sr.sprite = sprite1;
        else
            sr.sprite = sprite2;

        StartCoroutine(nameof(InitCorountine));
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        sr.size = new(0, 0.675f);
        Init(direction, info);
    }

    IEnumerator InitCorountine()
    {
        float duration = 0.35f;
        float timer = 0f;
        float len = info.bulletRange * GameData.GlobalRange;
        while (timer < duration)
        {
            if (timer > duration)
                timer = duration;
            sr.size = new(timer / duration * len, 0.675f);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        float x = sr.size.x;
        sr.size = new(x, 0);
        if (info.energyType == EnergyType.Cathode)
            sr.sprite = sprite11;
        else
            sr.sprite = sprite22;
        duration = 0.15f;
        timer = 0f;
        len = 1;
        while (timer < duration)
        {
            if (timer > duration)
                timer = duration;
            sr.size = new(x, timer / duration * len);
            timer += Time.deltaTime;
            yield return null;
        }
        box.enabled = true;
        box.offset = Vector2.right * x * 0.5f;
        box.size = new(x - 0.5f, 0.4f);
        duration = 0.1f;
        timer = 0f;
        len = 0;
        while (timer < duration)
        {
            if (timer > duration)
                timer = duration;
            sr.size = new(x, timer / duration * len);
            timer += Time.deltaTime;
            yield return null;
        }
        // box.enabled = false;
        // gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats.IsInv)
            {
                if (info.energyType == EnergyType.Anode)
                {
                    playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
                }
                else
                {
                    playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
                }
                playerStats.TakeDamage(info.atkMul);
            }
        }
    }
}
