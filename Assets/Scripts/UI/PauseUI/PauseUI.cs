using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameSetting gameSetting;
    public GameObject SoundBar_BGM;
    public GameObject SoundBar_SE;

    public void UpdateSoundBar()
    {
        SoundBar_BGM.GetComponent<Slider>().value = gameSetting.volume_BGM;
        SoundBar_SE.GetComponent<Slider>().value = gameSetting.volume_SE;
    }

    public void UpdateMusicMgr()
    {
        MusicMgr.Instance.BkValue = SoundBar_BGM.GetComponent<Slider>().value;
        MusicMgr.Instance.SoundValue = SoundBar_BGM.GetComponent<Slider>().value;
    }

    public void UpdateGameSetting()
    {
        gameSetting.volume_BGM = SoundBar_BGM.GetComponent<Slider>().value > 1 ? 1 : SoundBar_BGM.GetComponent<Slider>().value;
        gameSetting.volume_SE = SoundBar_SE.GetComponent<Slider>().value > 1 ? 1 : SoundBar_SE.GetComponent<Slider>().value;
    }

    public void OnContinueButtonPress()
    {
        UIManager.Instance.HidePauseUI();
    }

    public void ReStart()
    {
        SceneManager.LoadScene("TestByBaYYYA");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
