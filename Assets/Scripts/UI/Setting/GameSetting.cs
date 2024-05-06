using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSetting", fileName = "New Setting")]
public class GameSetting : ScriptableObject
{
    [Header("音量")]
    public float volume_BGM = 1;
    public float volume_SE = 1;

    [Header("初始属性")]
    [Header("主要属性")]
    public float default_maxHeath = 100;
    public float default_moveSpeed = 100;
    public float default_recoverForHealth = 0;
    public float default_defense = 3;
    public float default_critial = 0.05F;
    [Header("次要属性")]
    public float default_anodeEnergy = 10;
    public float default_cathodeEnergy = 10;
    public float default_powerOfAnode = 5;
    public float default_powerOfCathode = 5;
    public float default_criticalStrikeMultipiler = 1.5F;
    public float default_attackScatter = 1;
    public float default_energyConsumption = 1;
    public float default_piercingAttack = 1;

    [Header("作弊器参数")]
    [Header("主要属性")]
    public int multiple_maxHeath = 1;
    public int multiple_moveSpeed = 1;
    public int multiple_recoverForHealth = 1;
    public int multiple_defense = 1;
    public int multiple_critial = 1;

    [Header("次要属性")]
    public int multiple_anodeEnergy = 1;
    public int multiple_cathodeEnergy = 1;
    public int multiple_powerOfAnode = 1;
    public int multiple_powerOfCathode = 1;
    public int multiple_criticalStrikeMultipiler = 1;
    public int multiple_attackScatter = 1;
    public int multiple_energyConsumption = 1;
    public int multiple_piercingAttack = 1;

    [Header("初始魂魄")]
    public int initialCoins = 0;
    
}
