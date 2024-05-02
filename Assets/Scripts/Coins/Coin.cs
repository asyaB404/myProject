using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    public float coins = 1;

    public bool isTrigged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTrigged)
        {
            isTrigged = true;
            CoinsManager.Instance.Coins += coins;
            if (CoinsManager.Instance.tempCoins >= coins)
            {
                CoinsManager.Instance.tempCoins -= coins;
                CoinsManager.Instance.Coins += coins;
            }
            StartCoroutine(nameof(DestroyCoroutine));
        }
    }

    public void DoDestroy()
    {
        StartCoroutine(nameof(DestroyCoroutine));
    }

    float duration = 0.3f;

    IEnumerator DestroyCoroutine()
    {
        float timer = duration;
        while (timer > 0)
        {
            sr.color = new(1, 1, 1, timer / duration);
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
