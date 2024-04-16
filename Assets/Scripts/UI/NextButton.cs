using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public GameObject SelectedIcon;

    void Awake()
    {
        SelectedIcon.SetActive(false);
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
