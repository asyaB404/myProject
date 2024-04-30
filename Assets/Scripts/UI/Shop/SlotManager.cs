using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public Commodity curCommodity;
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void GenerateCommodity()
    {
        curCommodity = ShopManager.instance.GetRandomCommodity();
        while(ShopManager.Instance.curCommodities.Contains(curCommodity))
        {
            curCommodity = ShopManager.instance.GetRandomCommodity();
        }
        ShopManager.Instance.curCommodities.Add(curCommodity);

        transform.GetChild(0).GetComponent<Image>().color = ShopManager.Instance.qualityColor[curCommodity.quality - 1];
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = curCommodity.icon;
        transform.GetChild(1).GetComponent<Text>().text = curCommodity.commodityName;
        transform.GetChild(2).GetComponent<Text>().text = curCommodity.description;
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = curCommodity.price.ToString();
    }
    
    public void BuyCommodity()
    {
        if (CoinsManager.Instance.coins < curCommodity.price)
        {
            return;
        }
        CoinsManager.Instance.coins -= curCommodity.price;
        curCommodity.EffectAfterGet(playerStats);
        InventoryManager.Instance.AddItem(curCommodity);
        ShopManager.Instance.UpdateUI();
        gameObject.SetActive(false);
    }
}
