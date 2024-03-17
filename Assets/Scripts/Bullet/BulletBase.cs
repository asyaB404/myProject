using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private int penetrableCount;
    private energyType type;
    private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.type != type) return;
            enemy.TakeDamage(damage);
        }
    }

    public void SetupBullet(int _penetrableCount, energyType _type, float _damage)
    {
        this.damage = _damage;
        this.type = _type;
        this.penetrableCount = _penetrableCount;
    }
}
