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

    public GameObject[] slots;
    public SlotManager[] slotManagers;
    public Commodity[] commodities;

    public NextWaveButton nextWaveButton;
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
        foreach (GameObject slot in slots)
        {
            slot.SetActive(true);
        }
        foreach (SlotManager slot in slotManagers)
        {
            slot.GenerateCommodity();
        }
    }
}
