using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject SelectedIcon;

    void Awake()
    {
        SelectedIcon.SetActive(false);
    }

    public void OnPauseButtonPress()
    {
        if(Time.timeScale == 1)
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

    public void OnPointerEnter()
    {
        SelectedIcon.SetActive(true);
    }

    public void OnPointerExit()
    {
        SelectedIcon.SetActive(false);
    }
}
