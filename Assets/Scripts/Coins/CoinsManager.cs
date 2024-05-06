using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField]
    private GameSetting gameSetting;

    [SerializeField]
    private float coins;
    public float Coins
    {
        get { return coins; }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("coins_change", false);
            coins = value;
            MyEventSystem.Instance.EventTrigger<bool>("coins_change", true);
        }
    }
    public float tempCoins;
    public GameObject coinPrefab;
    public static CoinsManager Instance
    {
        get { return instance; }
    }
    private static CoinsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        LoadGameSetting();
    }

    public void GenerateCoin(Vector2 position, int count = 1, float value = 1)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.GetComponent<Coin>().coins = value;
            coin.transform.position =
                position + new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
        }
    }

    private void Update() { }

    public void Clear()
    {
        foreach (Transform coinsTransfrom in transform)
        {
            Coin coin = coinsTransfrom.GetComponent<Coin>();
            coin.isTrigged = true;
            Vector3 viewportPosition = new(0.05f, 0.6f, 0);
            Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
            coin.transform.DOLocalMove(worldPosition, 1.0f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tempCoins += coin.coins;
                });
            coin.transform.DOScale(0, 3f)
                .OnComplete(() =>
                {
                    coin.DoDestroy();
                });
        }
    }

    public void LoadGameSetting()
    {
        coins = gameSetting.initialCoins;
    }
}
