using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    public GameObject SelectedIcon;

    void Awake()
    {
        SelectedIcon.SetActive(false);
        Debug.Log("绑定成功");
        GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                LevelManager.Instance.StartNextLevel();
                UIManager.Instance.HideShopUI();
            });
    }

    public void UpdateWave()
    {
        GetComponentInChildren<Text>().text = (LevelManager.Instance.wave + 1).ToString();
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
