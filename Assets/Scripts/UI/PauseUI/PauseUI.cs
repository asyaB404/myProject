using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void OnContinueButtonPress()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
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
