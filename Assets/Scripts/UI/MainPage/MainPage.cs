using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainPage : MonoBehaviour
{
    public GameSetting gameSetting;
    public GameObject DifficultyUI;
    public GameObject SettingUI;
    public GameObject HowToPlayUI;
    public GameObject AboutUI;
    public GameObject CheaterUI;

    void Start()
    {
        MusicMgr.Instance.PlayBkMusic("bgm_theme");
        MusicMgr.Instance.BkValue = gameSetting.volume_BGM;
        MusicMgr.Instance.SoundValue = gameSetting.volume_SE;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(CheaterUI.activeSelf == false)
            {
                ShowCheaterUI();
            }
            else
            {
                HideCheaterUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingUI.activeSelf == true)
            {
                HideSettingUI();
            }
            if (AboutUI.activeSelf == true)
            {
                HideAboutUI();
            }
            if (HowToPlayUI.activeSelf == true)
            {
                HideHowToPlayUI();
            }
            if (CheaterUI.activeSelf == true)
            {
                HideCheaterUI();
            }
            if (DifficultyUI.activeSelf == true)
            {
                HideDifficultyUI();
            }
        }
    }

    public void GameStart(Difficulty difficulty)
    {
        gameSetting.SetDifficulty(difficulty);
        SceneManager.LoadScene("TestByBaYYYA");
    }

    public void GameStartEasy()
    {
        GameStart(Difficulty.Easy);
    }

    public void GameStartNormal()
    {
        GameStart(Difficulty.Normal);
    }

    public void GameStartHard()
    {
        GameStart(Difficulty.Hard);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowDifficultyUI()
    {
        DifficultyUI.SetActive(true);
    }

    public void HideDifficultyUI()
    {
        DifficultyUI.SetActive(false);
    }

    public void ShowSettingUI()
    {
        SettingUI.SetActive(true);
        SettingUI.GetComponent<SettingUI>().UpdateSoundBar();
    }

    public void HideSettingUI()
    {
        SettingUI.SetActive(false);
    }

    public void ShowCheaterUI()
    {
        CheaterUI.SetActive(true);
        CheaterUI.GetComponent<Cheater>().ReadGameSetting();
    }

    public void HideCheaterUI()
    {
        CheaterUI.GetComponent<Cheater>().UpdateGameSetting();
        CheaterUI.SetActive(false);
    }

    public void ShowAboutUI()
    {
        AboutUI.SetActive(true);
    }

    public void HideAboutUI()
    {
        AboutUI.SetActive(false);
    }

    public void ShowHowToPlayUI()
    {
        HowToPlayUI.SetActive(true);
    }

    public void HideHowToPlayUI()
    {
        HowToPlayUI.SetActive(false);
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
