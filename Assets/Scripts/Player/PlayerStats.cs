using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour 
{
    /// <summary>
    /// 主要属性
    /// </summary>
    [Header("主要属性")]
    public PlayerStat maxHealth;
    public PlayerStat curHealth;
    public PlayerStat moveSpeed;
    public PlayerStat recoverForHealth;
    public PlayerStat Defence;
    public PlayerStat Critical;

    /// <summary>
    /// 次要属性
    /// </summary>
    [Header("次要属性")]
    public PlayerStat anodeEnergy;//阳极能量
    public PlayerStat cathodeEnergy;//阴极能量
    public PlayerStat powerOfCathode;//阴灵之力
    public PlayerStat powerOfAnode;//阳灵之力
    public PlayerStat criticalStrikeMultiplier;//暴击倍率
    public PlayerStat attackScattering;//攻击散射
    public PlayerStat energyConsumption;//能量消耗
    public PlayerStat piercingAttack;//攻击穿透

    /// <summary>
    /// 承受伤害
    /// </summary>
    /// <param name="attackMultiple"></param>
    public void TakeDamage(int attackMultiple)
    {
        int damage = Mathf.Abs(Mathf.RoundToInt(anodeEnergy.GetValue())-Mathf.RoundToInt(cathodeEnergy.GetValue())) * attackMultiple;
        damage = damage - Mathf.RoundToInt(Defence.GetValue()) > 0 ? damage - Mathf.RoundToInt(Defence.GetValue()) : 0;
        curHealth.AddChange(-damage);
        if (curHealth.GetValue() < 0) Die();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        /// 动画播放死亡动画，在动画帧上添加事假
    }

}
