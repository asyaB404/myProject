using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public bool isPaused;
    public SlotManager[] slots;
    public Commodity[] commodities;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public Commodity getRandomCommodity()
    {
        int index = UnityEngine.Random.Range(0, commodities.Length);
        return commodities[index];
    }

    public void RefreshCommodities()
    {
        foreach (SlotManager slot in slots)
        {
            slot.GenerateCommodity();
        }
    }
    

    private void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    private void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
