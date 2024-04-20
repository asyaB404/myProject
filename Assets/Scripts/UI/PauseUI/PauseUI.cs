using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;
    public ShopManager shopManager;

    private void Awake()
    {
        shopManager.Init();
    }

    void Start()
    {
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
        }
    }

    public void OnContinueButtonPress()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
