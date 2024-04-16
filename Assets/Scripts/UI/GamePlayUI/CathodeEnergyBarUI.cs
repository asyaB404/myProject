using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CathodeEnergyBarUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider cathodeEnergyBarSlider;

    public Text value;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        cathodeEnergyBarSlider = GetComponent<Slider>();
        value = GetComponentInChildren<Text>();

        UpdateEnergyBarUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnergyBarUI();
    }

    private void UpdateEnergyBarUI()
    {
        float anodeEnergy = playerStats.anodeEnergy.GetValue();
        float cathodeEnergy = playerStats.cathodeEnergy.GetValue();
        cathodeEnergyBarSlider.maxValue = anodeEnergy + cathodeEnergy;
        cathodeEnergyBarSlider.value = cathodeEnergy;

        value.text = cathodeEnergy.ToString();
    }
}
