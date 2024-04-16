using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnodeEnergyBarUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider anodeEnergyBarSlider;

    public Text value;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        anodeEnergyBarSlider = GetComponent<Slider>();
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
        anodeEnergyBarSlider.maxValue = anodeEnergy + cathodeEnergy;
        anodeEnergyBarSlider.value = anodeEnergy;

        value.text = anodeEnergy.ToString();
    }
}
