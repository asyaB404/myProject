using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prop", fileName = "道具名")]
public class PropBase : ScriptableObject
{
    /// <summary>
    /// 主要属性
    /// </summary>
    [Header("主要属性")]
    public float maxHealth;
    public float curHealth;
    public float moveSpeed;
    public float recoverForHealth;
    public float Defence;
    public float Critical;

    /// <summary>
    /// 次要属性
    /// </summary>
    [Header("次要属性")]
    public float anodeEnergy; //阳极能量
    public float cathodeEnergy; //阴极能量
    public float powerOfCathode; //阴灵之力
    public float powerOfAnode; //阳灵之力
    public float criticalStrikeMultiplier; //暴击倍率
    public float attackScattering; //攻击散射
    public float energyConsumption; //能量消耗
    public float piercingAttack; //攻击穿透

    public virtual void EffectAfterGet(PlayerStats stats, GameSetting gameSetting)
    {
        stats.MaxHealth += maxHealth * gameSetting.multiple_maxHeath;
        stats.curHealth = stats.MaxHealth;
        stats.MoveSpeed += moveSpeed * gameSetting.multiple_moveSpeed;
        stats.RecoverForHealth += recoverForHealth * gameSetting.multiple_recoverForHealth;
        stats.Defence += Defence * gameSetting.multiple_defense;
        stats.Critical += Critical * gameSetting.multiple_critial;
        stats.AnodeEnergy += anodeEnergy * gameSetting.multiple_anodeEnergy;
        stats.CathodeEnergy += cathodeEnergy * gameSetting.multiple_cathodeEnergy;
        stats.PowerOfCathode += powerOfCathode * gameSetting.multiple_powerOfCathode;
        stats.PowerOfAnode += powerOfAnode * gameSetting.multiple_powerOfAnode;
        stats.CriticalStrikeMultiplier += criticalStrikeMultiplier * gameSetting.multiple_criticalStrikeMultipiler;
        stats.AttackScattering += attackScattering * gameSetting.multiple_attackScatter;
        stats.EnergyConsumption += energyConsumption * gameSetting.multiple_energyConsumption;
        stats.PiercingAttack += piercingAttack * gameSetting.multiple_piercingAttack;
    }
}
