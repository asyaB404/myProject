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
        Text_maxHealth.text = gameSetting.multiple_maxHeath.ToString();
        Text_moveSpeed.text = gameSetting.multiple_moveSpeed.ToString();
        Text_recoverForHealth.text = gameSetting.multiple_recoverForHealth.ToString();
        Text_defense.text = gameSetting.multiple_defense.ToString();
        Text_critial.text = gameSetting.multiple_critial.ToString();
        Text_anodeEnergy.text = gameSetting.multiple_anodeEnergy.ToString();
        Text_cathodeEnergy.text = gameSetting.multiple_cathodeEnergy.ToString();
        Text_powerOfAnode.text = gameSetting.multiple_powerOfAnode.ToString();
        Text_powerOfCathode.text = gameSetting.multiple_powerOfCathode.ToString();
        Text_criticalStrikeMultipiler.text = gameSetting.multiple_criticalStrikeMultipiler.ToString();
        Text_attackScatter.text = gameSetting.multiple_attackScatter.ToString();
        Text_energyConsumption.text = gameSetting.multiple_energyConsumption.ToString();
        Text_piercingAttack.text = gameSetting.multiple_piercingAttack.ToString();
        Text_initialCoins.text = gameSetting.initialCoins.ToString();
    }

    public void UpdateGameSetting()
    {
        gameSetting.multiple_maxHeath = Text_maxHealth.text == null ? 1 : int.Parse(Text_maxHealth.text);
        gameSetting.multiple_moveSpeed = Text_moveSpeed.text == null ? 1 : int.Parse(Text_moveSpeed.text);
        gameSetting.multiple_recoverForHealth = Text_recoverForHealth.text == null ? 1 : int.Parse(Text_recoverForHealth.text);
        gameSetting.multiple_defense = Text_defense.text == null ? 1 : int.Parse(Text_defense.text);
        gameSetting.multiple_critial = Text_critial.text == null ? 1 : int.Parse(Text_critial.text);
        gameSetting.multiple_anodeEnergy = Text_anodeEnergy.text == null ? 1 : int.Parse(Text_anodeEnergy.text);
        gameSetting.multiple_cathodeEnergy = Text_cathodeEnergy.text == null ? 1 : int.Parse(Text_cathodeEnergy.text);
        gameSetting.multiple_powerOfAnode = Text_powerOfAnode.text == null ? 1 : int.Parse(Text_powerOfAnode.text);
        gameSetting.multiple_powerOfCathode = Text_powerOfCathode.text == null ? 1 : int.Parse(Text_powerOfCathode.text);
        gameSetting.multiple_criticalStrikeMultipiler = Text_criticalStrikeMultipiler.text == null ? 1 : int.Parse(Text_criticalStrikeMultipiler.text);
        gameSetting.multiple_attackScatter = Text_attackScatter.text == null ? 1 : int.Parse(Text_attackScatter.text);
        gameSetting.multiple_energyConsumption = Text_energyConsumption.text == null ? 1 : int.Parse(Text_energyConsumption.text);
        gameSetting.multiple_piercingAttack = Text_piercingAttack.text == null ? 1 : int.Parse(Text_piercingAttack.text);
        gameSetting.initialCoins = Text_initialCoins.text == null ? 0 : int.Parse(Text_initialCoins.text);
    }
}
