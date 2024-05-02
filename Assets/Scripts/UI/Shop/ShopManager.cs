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

    public List<Commodity> randomPool;
    public int minQuality = 1;
    public int maxQuality = 5;

    public Color[] qualityColor = {
        new Color(1.0f, 1.0f, 1.0f, 0.4f),
        new Color(0.2f, 0.7f, 0.4f, 0.4f),
        new Color(0.3f, 0.45f, 0.7f, 0.4f),
        new Color(0.6f, 0.4f, 0.6f, 0.4f),
        new Color(0.9f, 0.6f, 0.4f, 0.4f)
    };

    public void Init()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        foreach(Commodity commodity in commodities)
        {
            commodity.holdNum = 0;
        }
    }

    private void Awake()
    {
        Init();
    }

    public void OnEnter()
    {
        RefreshRandomPool();
        refreshTimes = 0;
        refreshPrice = priceList[0];
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

    public void UpdateUI()
    {
        nextWaveButton.UpdateWave();
        refreshButton.UpdateRefreshPrice();
        playerStatUI.UpdatePlayerStats();
        shopCurSpiritUI.UpdateSpirit();
        InventoryManager.Instance.refreshInventoryUI();
    }

    public Commodity GetRandomCommodity()
    {
        int index = UnityEngine.Random.Range(0, randomPool.Count);
        return randomPool[index];
    }

    public void RefreshCommodities()
    {
        if(CoinsManager.Instance.Coins < refreshPrice)
        {
            return;
        }

        CoinsManager.Instance.Coins -= refreshPrice;
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

    public void RefreshRandomPool()
    {
        if(LevelManager.Instance.wave == 1)
        {
            minQuality = 1;
            maxQuality = 1;
        } else if (LevelManager.Instance.wave >= 2 && LevelManager.Instance.wave<=5)
        {
            minQuality = 1;
            maxQuality = 2;
        } else if (LevelManager.Instance.wave >= 6 && LevelManager.Instance.wave <=10)
        {
            minQuality = 1;
            maxQuality = 3;
        } else if (LevelManager.Instance.wave >= 11 && LevelManager.Instance.wave <= 15)
        {
            minQuality = 2;
            maxQuality = 4;
        } else if (LevelManager.Instance.wave >= 16 && LevelManager.Instance.wave <= 17)
        {
            minQuality = 2;
            maxQuality = 5;
        } else
        {
            minQuality = 3;
            maxQuality = 5;
        }

        randomPool.Clear();
        foreach (Commodity commodity in commodities)
        {
            if (commodity.quality >= minQuality && commodity.quality <= maxQuality)
            {
                randomPool.Add(commodity);
            }
        }
    }
}
