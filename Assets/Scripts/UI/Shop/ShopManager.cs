using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public static ShopManager Instance
    {
        get => instance;
    }

    public int refreshTimes = 0;
    public int refreshPrice = 0;
    public List<int> priceList;

    public GameObject[] slots;
    public SlotManager[] slotManagers;
    public Commodity[] commodities;
    public List<Commodity> curCommodities;

    public NextWaveButton nextWaveButton;
    public RefreshButton refreshButton;
    public PlayerStatUI playerStatUI;
    public ShopCurSpiritUI shopCurSpiritUI;

    public void Init()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        foreach (SlotManager slot in slotManagers)
        {
            slot.GenerateCommodity();
        }
    }

    public void UpdateUI()
    {
        nextWaveButton.UpdateWave();
        refreshButton.UpdateRefreshPrice();
        playerStatUI.UpdatePlayerStats();
        shopCurSpiritUI.UpdateSpirit();
    }

    public Commodity GetRandomCommodity()
    {
        int index = UnityEngine.Random.Range(0, commodities.Length);
        return commodities[index];
    }

    public void RefreshCommodities()
    {
        if(CoinsManager.Instance.coins < refreshPrice)
        {
            return;
        }

        CoinsManager.Instance.coins -= refreshPrice;
        refreshTimes++;
        if(refreshTimes >= priceList.Count)
        {
            refreshTimes = priceList.Count - 1;
        }
        refreshPrice = priceList[refreshTimes];

        curCommodities.Clear();
        foreach (GameObject slot in slots)
        {
            slot.SetActive(true);
        }
        foreach (SlotManager slot in slotManagers)
        {
            slot.GenerateCommodity();
        }

        UpdateUI();
    }
}
