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
        UIManager.Instance.ShowPauseUI();
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
