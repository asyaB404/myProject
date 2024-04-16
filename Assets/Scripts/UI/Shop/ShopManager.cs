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

    private void Start()
    {
        foreach(GameObject slot in slots)
        {
            slot.SetActive(true);
        }
        foreach(SlotManager slot in slotManagers)
        {
            slot.GenerateCommodity();
        }
    }

    public Commodity getRandomCommodity()
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
