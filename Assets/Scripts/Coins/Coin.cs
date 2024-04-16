using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    public int coins = 1;

    //保证一个金币只能被触发一次;
    public bool isTrigged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayersCoins>(out PlayersCoins playersCoins) && !isTrigged)
        {
            isTrigged = true;
            playersCoins.coins += coins;
            if (GameData.Instance.TempCoins >= coins)
            {
                GameData.Instance.TempCoins -= coins;
                playersCoins.coins += coins;
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
}
