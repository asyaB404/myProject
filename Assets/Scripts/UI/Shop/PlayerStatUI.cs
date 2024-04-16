using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        UpdatePlayerStats();
    }

    private void Update()
    {
        UpdatePlayerStats();
    }

    public void UpdatePlayerStats()
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = playerStats.maxHealth.GetValue().ToString();
        transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = playerStats.moveSpeed.GetValue().ToString();
        transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = playerStats.recoverForHealth.GetValue().ToString();
        transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = playerStats.Defence.GetValue().ToString();
        transform.GetChild(4).GetComponent<UnityEngine.UI.Text>().text = playerStats.Critical.GetValue().ToString();
        transform.GetChild(5).GetComponent<UnityEngine.UI.Text>().text = playerStats.anodeEnergy.GetValue().ToString();
        transform.GetChild(6).GetComponent<UnityEngine.UI.Text>().text = playerStats.cathodeEnergy.GetValue().ToString();
        transform.GetChild(7).GetComponent<UnityEngine.UI.Text>().text = playerStats.powerOfCathode.GetValue().ToString();
        transform.GetChild(8).GetComponent<UnityEngine.UI.Text>().text = playerStats.powerOfAnode.GetValue().ToString();
        transform.GetChild(9).GetComponent<UnityEngine.UI.Text>().text = playerStats.criticalStrikeMultiplier.GetValue().ToString();
        transform.GetChild(10).GetComponent<UnityEngine.UI.Text>().text = playerStats.attackScattering.GetValue().ToString();
        transform.GetChild(11).GetComponent<UnityEngine.UI.Text>().text = playerStats.energyConsumption.GetValue().ToString();
        transform.GetChild(12).GetComponent<UnityEngine.UI.Text>().text = playerStats.piercingAttack.GetValue().ToString();
    }
}
