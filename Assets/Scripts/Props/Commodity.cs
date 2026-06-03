using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Commodity", fileName = "������")]
public class Commodity : PropBase
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Header("商品信息")]
    public int id;
    public int type;
    public string commodityName;
    public Sprite icon;
    public float price;
    public int quality;
    public string description;

    [Header("持有数量")]
    public int holdNum;

    //没时间重构道具框架了，只能堆屎山了
    public override void EffectAfterGet(PlayerStats stats, GameSetting gameSetting)
    {
        base.EffectAfterGet(stats, gameSetting);
        if (id < 20038)
            return;

        switch (id)
        {
            case 20038:
                stats.EnergyConsumption = 1;
                break;
            case 20039:
                stats.AnodeEnergy += Mathf.FloorToInt(stats.MaxHealth * 0.5f);
                break;
            case 20040:
                stats.PowerOfAnode += Mathf.FloorToInt(stats.MaxHealth * 0.1f);
                break;
            case 20041:
                stats.CathodeEnergy += Mathf.FloorToInt(stats.RecoverForHealth * 20);
                break;
            case 20042:
                stats.PowerOfCathode += Mathf.FloorToInt(stats.RecoverForHealth * 2);
                break;
            case 20043:
                if (stats.CathodeEnergy > stats.AnodeEnergy)
                    stats.AnodeEnergy = stats.CathodeEnergy;
                else
                    stats.CathodeEnergy = stats.AnodeEnergy;
                break;
            case 20044:
                MyEventSystem.Instance.AddEventListener<bool>(
                    GameEventType.EnergyChange,
                    (flag) =>
                    {
                        float temp = Mathf.Abs(stats.AnodeEnergy - stats.CathodeEnergy);
                        if (!flag)
                        {
                            stats.powerOfAnode -= temp;
                            stats.powerOfCathode -= temp;
                        }
                        else
                        {
                            stats.powerOfAnode += temp;
                            stats.powerOfCathode += temp;
                        }
                    }
                );
                {
                    float temp = Mathf.Abs(stats.AnodeEnergy - stats.CathodeEnergy);
                    stats.powerOfAnode += temp;
                    stats.powerOfCathode += temp;
                }
                break;
            case 20045:
                MyEventSystem.Instance.AddEventListener<bool>(
                    GameEventType.DefChange,
                    (flag) =>
                    {
                        float temp = stats.Defence * 0.01f;
                        if (!flag)
                            stats.criticalStrikeMultiplier -= temp;
                        else
                            stats.criticalStrikeMultiplier += temp;
                    }
                );
                {
                    float temp = stats.Defence * 0.01f;
                    stats.criticalStrikeMultiplier += temp;
                }
                break;
            case 20046:
                MyEventSystem.Instance.AddEventListener<bool>(
                    GameEventType.CriChange,
                    (flag) =>
                    {
                        float temp = 1 - stats.Critical;
                        if (!flag)
                            stats.criticalStrikeMultiplier -= temp;
                        else
                            stats.criticalStrikeMultiplier += temp;
                    }
                );
                {
                    float temp = 1 - stats.Critical;
                    stats.criticalStrikeMultiplier += temp;
                }
                break;
            case 20047:
                stats.CriticalStrikeMultiplier += (stats.MoveSpeed - 110) * 0.01f;
                break;
            case 20048:
                MyEventSystem.Instance.AddEventListener<bool>(
                    GameEventType.HpChange,
                    (flag) =>
                    {
                        float temp = (stats.MaxHealth - stats.CurHealth) * 0.5f;
                        if (!flag)
                        {
                            stats.powerOfAnode -= temp;
                            stats.powerOfCathode -= temp;
                        }
                        else
                        {
                            stats.powerOfAnode += temp;
                            stats.powerOfCathode += temp;
                        }
                    }
                );
                {
                    float temp = (stats.MaxHealth - stats.CurHealth) * 0.5f;
                    stats.powerOfAnode += temp;
                    stats.powerOfCathode += temp;
                }
                break;
            case 20049:
                {
                    float coins = CoinsManager.Instance.Coins;
                    stats.PowerOfAnode += coins;
                    stats.PowerOfCathode += coins;
                    void Fun()
                    {
                        stats.PowerOfAnode -= coins;
                        stats.PowerOfCathode -= coins;
                        MyEventSystem.Instance.RemoveEventListener(GameEventType.LevelClear, Fun);
                    }
                    MyEventSystem.Instance.AddEventListener(GameEventType.LevelClear, Fun);
                }
                break;
            case 20050:
                stats.SwordCount += 2;
                break;
            case 20052:
                MyEventSystem.Instance.AddEventListener<EnemyBase>(
                    GameEventType.MonsDie,
                    (EnemyBase enemy) =>
                    {
                        if (enemy.info.energyType == EnergyType.Cathode)
                            stats.CathodeEnergy += 1;
                    }
                );
                break;
            case 20053:
                MyEventSystem.Instance.AddEventListener<EnemyBase>(
                    GameEventType.MonsDie,
                    (EnemyBase enemy) =>
                    {
                        if (enemy.info.energyType == EnergyType.Anode)
                            stats.AnodeEnergy += 1;
                    }
                );
                break;
        }
    }
}
