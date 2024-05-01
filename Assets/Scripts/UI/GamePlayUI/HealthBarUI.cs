using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider healthBarSlider;

    public Text value;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthBarSlider = GetComponent<Slider>();
        value = GetComponentInChildren<Text>();

        UpdateHealthBarUI();
    }

    void Update()
    {
        UpdateHealthBarUI();
    }

    private void UpdateHealthBarUI()
    {
        float maxHealth = playerStats.maxHealth.GetValue();
        float curHealth = playerStats.curHealth.GetValue();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = curHealth;

        value.text = Mathf.FloorToInt(curHealth) + "/" + Mathf.FloorToInt(maxHealth);
    }
}
