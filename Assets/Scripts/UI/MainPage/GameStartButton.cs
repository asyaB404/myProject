using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public void GameStart()
    {
        UIManager.Instance.HideMainPageUI();
        UIManager.Instance.ShowGamePlayUI();
        if(ShopManager.Instance)
        {
            ShopManager.Instance.Reset();
        
        }
        LevelManager.Instance.StartNextLevel();
    }

    public void OnPointerEnter()
    {
        MusicMgr.Instance.PlaySound("hover");
    }
}
