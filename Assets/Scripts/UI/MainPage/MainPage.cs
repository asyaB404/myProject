using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPage : MonoBehaviour
{
    public GameSetting gameSetting;
    public GameObject SettingUI;
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
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("TestByBaYYYA");
    }

    public void Exit()
    {
        Application.Quit();
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

    public void PlayHoverSound()
    {
        MusicMgr.Instance.PlaySound("hover");
    }
    
    public void PlayClickSound()
    {
        MusicMgr.Instance.PlaySound("click");
    }
}
