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
        GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                LevelManager.Instance.StartNextLevel();
                ShopManager.Instance.refreshTimes = 0;
                ShopManager.Instance.refreshPrice = 0;
                UIManager.Instance.HideShopUI();
            });
    }

    private void OnEnable() {
        
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
