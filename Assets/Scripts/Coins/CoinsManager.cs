using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
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
    }

    public void GenerateCoin(Vector2 position, int count = 1, int value = 1)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.GetComponent<Coin>().coins = value;
            coin.transform.position = new(
                position.x + Random.Range(0, 0.2f),
                position.y + Random.Range(0, 0.2f)
            );
        }
    }

    public void Clear()
    {
        foreach (Transform coinsTransfrom in transform)
        {
            Coin coin = coinsTransfrom.GetComponent<Coin>();
            coin.DoDestroy();
            // transform.DOMove(Camera.main.ScreenToWorldPoint);
        }
    }
}
