using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public bool isPaused;
    public GameObject[] slots;
    public SlotManager[] slotManagers;
    public Commodity[] commodities;

    public void Init()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    private void Start()
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

    public void HideMe()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowMe()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
