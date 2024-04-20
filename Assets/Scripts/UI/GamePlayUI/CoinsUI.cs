using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    public Text coin1;
    public Text coin2;

    void Update()
    {
        coin1.text = CoinsManager.Instance.coins.ToString("F1");
        coin2.text = CoinsManager.Instance.tempCoins.ToString("F1");
    }
}
