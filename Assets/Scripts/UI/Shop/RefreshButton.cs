using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour
{
    public Text value;

    private void Start()
    {
        value.text = ShopManager.Instance.refreshPrice.ToString();
    }

    public void UpdateRefreshPrice()
    {
        value.text = ShopManager.Instance.refreshPrice.ToString();
    }
}
