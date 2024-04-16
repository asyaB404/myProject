using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayersCoins : MonoBehaviour
{
    public int coins;
    public float pullingRange = 5f;

    // private Vector3 Direction{get{return }}

    private void Update()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, pullingRange, 1 << 6);
        foreach (var obj in objs)
        {
            Vector2 direction = (transform.position - obj.transform.position).normalized;
            float len = (transform.position - obj.transform.position).magnitude;
            obj.transform.Translate(direction * Time.deltaTime * (len + 2));
            // obj.transform.DOLocalMove(transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pullingRange);
    }
}
