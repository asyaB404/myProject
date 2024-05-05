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

    public virtual void EffectAfterGet(PlayerStats stats)
    {
        stats.MaxHealth += maxHealth;
        stats.CurHealth = stats.MaxHealth;
        stats.MoveSpeed += moveSpeed;
        stats.RecoverForHealth += recoverForHealth;
        stats.Defence += Defence;
        stats.Critical += Critical;
        stats.AnodeEnergy += anodeEnergy;
        stats.CathodeEnergy += cathodeEnergy;
        stats.PowerOfCathode += powerOfCathode;
        stats.PowerOfAnode += powerOfAnode;
        stats.CriticalStrikeMultiplier += criticalStrikeMultiplier;
        stats.AttackScattering += attackScattering;
        stats.EnergyConsumption += energyConsumption;
        stats.PiercingAttack += piercingAttack;
    }
}
