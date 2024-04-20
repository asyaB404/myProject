using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
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
                ShopManager.instance.HideMe();
            });
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
