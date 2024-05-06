using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UIState
{
    Shop,
    GamePlay,
    GameOver
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIState currentState = UIState.GamePlay;
    public GameObject GamePlayUI;
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

    void Start()
    {
        ShowGamePlayUI();
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

    public void ShowGamePlayUI()
    {
        Time.timeScale = 1;
        currentState = UIState.GamePlay;
        GamePlayUI.SetActive(true);
        UpdateBackGroundMusic();
    }

    public void HideGamePlayUI()
    {
        GamePlayUI.SetActive(false);
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

    public void ShowGameOverUI(bool isVictory)
    {
        Time.timeScale = 0;
        currentState = UIState.GameOver;
        UpdateBackGroundMusic();
        GameOverUI.SetActive(true);
        GameOverUI.GetComponent<GameOverUI>().SetBaseMap(isVictory);
    }

    public void BackToMainPage()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainPage");
    }

    public void PlayHoverSound()
    {
        MusicMgr.Instance.PlaySound("hover");
    }

    public void PlayClickSound()
    {
        MusicMgr.Instance.PlaySound("click");
    }

}
