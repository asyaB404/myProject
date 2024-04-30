using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Commodity curItem;

    public void SetItem(Commodity item)
    {
        curItem = item;
        transform.GetComponent<Image>().color = ShopManager.Instance.qualityColor[item.quality - 1];
        transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        transform.GetChild(1).GetComponent<Text>().text = item.holdNum.ToString();
    }
}
