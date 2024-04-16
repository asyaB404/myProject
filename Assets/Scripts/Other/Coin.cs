using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private int coins = 1;
    public int Coins
    {
        get { return coins; }
        set
        {
            Vector3 scale = Vector3.zero;
            if (coins <= 10)
            {
                scale = Vector3.one * (0.9f + 0.1f * coins);
            }
            else
            {
                scale = Vector2.one * 2;
            }
            transform.localScale = scale;
        }
    }

    //保证一个金币只能被触发一次;
    public bool haveTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayersCoins>(out PlayersCoins playersCoins) && !haveTrigger)
        {
            haveTrigger = true;
            playersCoins.coins += coins;
            StartCoroutine(nameof(DOFadeCoroutine));
        }
    }

    float duration = 0.5f;

    IEnumerator DOFadeCoroutine()
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
