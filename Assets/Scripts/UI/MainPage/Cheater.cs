using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheater : MonoBehaviour
{
    public GameSetting gameSetting;

    [Header("作弊器参数")]
    public InputField Text_maxHealth;
    public InputField Text_moveSpeed;
    public InputField Text_recoverForHealth;
    public InputField Text_defense;
    public InputField Text_critial;
    public InputField Text_anodeEnergy;
    public InputField Text_cathodeEnergy;
    public InputField Text_powerOfAnode;
    public InputField Text_powerOfCathode;
    public InputField Text_criticalStrikeMultipiler;
    public InputField Text_attackScatter;
    public InputField Text_energyConsumption;
    public InputField Text_piercingAttack;
    public InputField Text_initialCoins;

    public void ReadGameSetting()
    {
        Text_maxHealth.text = gameSetting.addition_maxHeath.ToString();
        Text_moveSpeed.text = gameSetting.addition_moveSpeed.ToString();
        Text_recoverForHealth.text = gameSetting.addition_recoverForHealth.ToString();
        Text_defense.text = gameSetting.addition_defense.ToString();
        Text_critial.text = gameSetting.addition_critial.ToString();
        Text_anodeEnergy.text = gameSetting.addition_anodeEnergy.ToString();
        Text_cathodeEnergy.text = gameSetting.addition_cathodeEnergy.ToString();
        Text_powerOfAnode.text = gameSetting.addition_powerOfAnode.ToString();
        Text_powerOfCathode.text = gameSetting.addition_powerOfCathode.ToString();
        Text_criticalStrikeMultipiler.text = gameSetting.addition_criticalStrikeMultipiler.ToString();
        Text_attackScatter.text = gameSetting.addition_attackScatter.ToString();
        Text_energyConsumption.text = gameSetting.addition_energyConsumption.ToString();
        Text_piercingAttack.text = gameSetting.addition_piercingAttack.ToString();
        Text_initialCoins.text = gameSetting.initialCoins.ToString();
    }

    public void UpdateGameSetting()
    {
        gameSetting.addition_maxHeath = Text_maxHealth.text == null ? 0 : int.Parse(Text_maxHealth.text);
        gameSetting.addition_moveSpeed = Text_moveSpeed.text == null ? 0 : int.Parse(Text_moveSpeed.text);
        gameSetting.addition_recoverForHealth = Text_recoverForHealth.text == null ? 0 : int.Parse(Text_recoverForHealth.text);
        gameSetting.addition_defense = Text_defense.text == null ? 0 : int.Parse(Text_defense.text);
        gameSetting.addition_critial = Text_critial.text == null ? 0 : int.Parse(Text_critial.text);
        gameSetting.addition_anodeEnergy = Text_anodeEnergy.text == null ? 0 : int.Parse(Text_anodeEnergy.text);
        gameSetting.addition_cathodeEnergy = Text_cathodeEnergy.text == null ? 0 : int.Parse(Text_cathodeEnergy.text);
        gameSetting.addition_powerOfAnode = Text_powerOfAnode.text == null ? 0 : int.Parse(Text_powerOfAnode.text);
        gameSetting.addition_powerOfCathode = Text_powerOfCathode.text == null ? 0 : int.Parse(Text_powerOfCathode.text);
        gameSetting.addition_criticalStrikeMultipiler = Text_criticalStrikeMultipiler.text == null ? 0 : int.Parse(Text_criticalStrikeMultipiler.text);
        gameSetting.addition_attackScatter = Text_attackScatter.text == null ? 0 : int.Parse(Text_attackScatter.text);
        gameSetting.addition_energyConsumption = Text_energyConsumption.text == null ? 0 : int.Parse(Text_energyConsumption.text);
        gameSetting.addition_piercingAttack = Text_piercingAttack.text == null ? 0 : int.Parse(Text_piercingAttack.text);
        gameSetting.initialCoins = Text_initialCoins.text == null ? 0 : int.Parse(Text_initialCoins.text);
    }
}
