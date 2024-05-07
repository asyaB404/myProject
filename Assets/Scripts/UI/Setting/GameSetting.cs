using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSetting", fileName = "New Setting")]
public class GameSetting : ScriptableObject
{
    [Header("难度")]
    public Difficulty difficulty = Difficulty.Normal;
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

    [Header("初始魂魄")]
    public float default_coins = 0;

    [Header("作弊器参数")]
    [Header("主要属性")]
    public int addition_maxHeath = 0;
    public int addition_moveSpeed = 0;
    public int addition_recoverForHealth = 0;
    public int addition_defense = 0;
    public int addition_critial = 0;

    [Header("次要属性")]
    public int addition_anodeEnergy = 0;
    public int addition_cathodeEnergy = 0;
    public int addition_powerOfAnode = 0;
    public int addition_powerOfCathode = 0;
    public int addition_criticalStrikeMultipiler = 0;
    public int addition_attackScatter = 0;
    public int addition_energyConsumption = 0;
    public int addition_piercingAttack = 0;

    [Header("初始魂魄")]
    public int initialCoins = 0;

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        if(difficulty == Difficulty.Easy)
        {
            default_maxHeath = 100;
            default_moveSpeed = 100;
            default_recoverForHealth = 0;
            default_defense = 3;
            default_critial = 0.05F;
            default_anodeEnergy = 10;
            default_cathodeEnergy = 10;
            default_powerOfAnode = 10;
            default_powerOfCathode = 10;
            default_criticalStrikeMultipiler = 1.5F;
            default_attackScatter = 1;
            default_energyConsumption = 1;
            default_piercingAttack = 1;
            default_coins = 50;
        } 
        else if (difficulty == Difficulty.Normal)
        {
            default_maxHeath = 100;
            default_moveSpeed = 100;
            default_recoverForHealth = 0;
            default_defense = 3;
            default_critial = 0.05F;
            default_anodeEnergy = 10;
            default_cathodeEnergy = 10;
            default_powerOfAnode = 5;
            default_powerOfCathode = 5;
            default_criticalStrikeMultipiler = 1.5F;
            default_attackScatter = 1;
            default_energyConsumption = 1;
            default_piercingAttack = 1;
            default_coins = 30;
        }
        else
        {
            default_maxHeath = 100;
            default_moveSpeed = 100;
            default_recoverForHealth = 0;
            default_defense = 3;
            default_critial = 0.05F;
            default_anodeEnergy = 10;
            default_cathodeEnergy = 10;
            default_powerOfAnode = 5;
            default_powerOfCathode = 5;
            default_criticalStrikeMultipiler = 1.5F;
            default_attackScatter = 1;
            default_energyConsumption = 1;
            default_piercingAttack = 1;
            default_coins = 0;
        }
    }
    
}
