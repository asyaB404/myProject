using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurSpiritUI : MonoBehaviour
{
    public void UpdateSpirit()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = CoinsManager.Instance.Coins.ToString();
    }
}
