using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject PauseUI;
    public GameObject ShopUI;

    public static UIManager Instance
    {
        get => instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                ShowPauseUI();
            }
            else
            {
                HidePauseUI();
            }
        }
    }

    public void ShowShopUI()
    {
        Time.timeScale = 0;
        ShopUI.SetActive(true);
        ShopManager.Instance.OnEnter();
    }

    public void HideShopUI()
    {
        Time.timeScale = 1;
        ShopUI.SetActive(false);
    }

    public void ShowPauseUI()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }
}
