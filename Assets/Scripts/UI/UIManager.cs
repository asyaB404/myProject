using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UIState
{
    MainPage,
    Shop,
    GamePlay,
    GameOver
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIState currentState = UIState.GamePlay;

    public GameObject PauseUI;
    public GameObject ShopUI;
    public GameObject GameOverUI;

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
            if (PauseUI.activeSelf == false)
            {
                ShowPauseUI();
            }
            else
            {
                HidePauseUI();
            }
        }
    }

    public void UpdateBackGroundMusic()
    {
        MusicMgr.Instance.StopMusic();
        if(currentState == UIState.GamePlay)
        {
            MusicMgr.Instance.PlayBkMusic("bgm_level");
        }
        else if(currentState == UIState.Shop)
        {
            MusicMgr.Instance.PlayBkMusic("bgm_shop");
        }
        else
        {
            MusicMgr.Instance.PlayBkMusic("bgm_theme");
        }
    }

    public void ShowShopUI()
    {
        Time.timeScale = 0;
        ShopUI.SetActive(true);
        ShopManager.Instance.OnEnter();
        currentState = UIState.Shop;
        UpdateBackGroundMusic();
    }

    public void HideShopUI()
    {
        Time.timeScale = 1;
        ShopUI.SetActive(false);
    }

    public void ShowPauseUI()
    {
        if(currentState == UIState.GamePlay)
        {
            Time.timeScale = 0;
        }
        PauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        if(currentState == UIState.GamePlay)
        {
            Time.timeScale = 1;
        }
        PauseUI.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        currentState = UIState.GameOver;
        UpdateBackGroundMusic();
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }
}
