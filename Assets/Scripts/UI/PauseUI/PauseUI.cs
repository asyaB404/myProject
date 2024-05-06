using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void UpdateGameSetting()
    {
        gameSetting.volume_BGM = SoundBar_BGM.GetComponent<Slider>().value;
        MusicMgr.Instance.BkValue = gameSetting.volume_BGM;
        gameSetting.volume_SE = SoundBar_SE.GetComponent<Slider>().value;
        MusicMgr.Instance.SoundValue = gameSetting.volume_SE;
    }

    public void OnContinueButtonPress()
    {
        UIManager.Instance.HidePauseUI();
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
