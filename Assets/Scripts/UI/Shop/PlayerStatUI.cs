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

    public void UpdatePlayerStats()
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = playerStats.MaxHealth.ToString();
        transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = playerStats.MoveSpeed.ToString();
        transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = playerStats.RecoverForHealth.ToString();
        transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = playerStats.Defence.ToString();
        transform.GetChild(4).GetComponent<UnityEngine.UI.Text>().text = playerStats.Critical.ToString();
        transform.GetChild(5).GetComponent<UnityEngine.UI.Text>().text = playerStats.AnodeEnergy.ToString();
        transform.GetChild(6).GetComponent<UnityEngine.UI.Text>().text = playerStats.CathodeEnergy.ToString();
        transform.GetChild(7).GetComponent<UnityEngine.UI.Text>().text = playerStats.PowerOfCathode.ToString();
        transform.GetChild(8).GetComponent<UnityEngine.UI.Text>().text = playerStats.PowerOfAnode.ToString();
        transform.GetChild(9).GetComponent<UnityEngine.UI.Text>().text = playerStats.CriticalStrikeMultiplier.ToString();
        transform.GetChild(10).GetComponent<UnityEngine.UI.Text>().text = playerStats.AttackScattering.ToString();
        transform.GetChild(11).GetComponent<UnityEngine.UI.Text>().text = playerStats.EnergyConsumption.ToString();
        transform.GetChild(12).GetComponent<UnityEngine.UI.Text>().text = playerStats.PiercingAttack.ToString();
    }
}
