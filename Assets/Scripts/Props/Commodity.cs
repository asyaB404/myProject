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
    public override void EffectAfterGet(PlayerStats stats)
    {
        base.EffectAfterGet(stats);
        if (id < 20038)
        {
            return;
        }
        if (id == 20038)
        {
            stats.EnergyConsumption = 1;
        }
        else if (id == 20039)
        {
            stats.AnodeEnergy += Mathf.FloorToInt(stats.MaxHealth * 0.5f);
        }
        else if (id == 20040)
        {
            stats.PowerOfAnode += Mathf.FloorToInt(stats.MaxHealth * 0.1f);
        }
        else if (id == 20041)
        {
            stats.CathodeEnergy += Mathf.FloorToInt(stats.RecoverForHealth * 20);
        }
        else if (id == 20042)
        {
            stats.PowerOfCathode += Mathf.FloorToInt(stats.RecoverForHealth * 2);
        }
        else if (id == 20043)
        {
            if (stats.CathodeEnergy > stats.AnodeEnergy)
            {
                stats.AnodeEnergy = stats.CathodeEnergy;
            }
            else
            {
                stats.CathodeEnergy = stats.AnodeEnergy;
            }
        }
        else if (id == 20044)
        {
            MyEventSystem.Instance.AddEventListener<bool>(
                "energy_change",
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
            MyEventSystem.Instance.EventTrigger<bool>("energy_change", true);
        }
        else if (id == 20045)
        {
            MyEventSystem.Instance.AddEventListener<bool>(
                "def_change",
                (flag) =>
                {
                    float temp = stats.Defence * 0.01f;
                    if (!flag)
                    {
                        stats.criticalStrikeMultiplier -= temp;
                    }
                    else
                    {
                        stats.criticalStrikeMultiplier += temp;
                    }
                }
            );
            MyEventSystem.Instance.EventTrigger<bool>("def_change", true);
        }
        else if (id == 20046)
        {
            MyEventSystem.Instance.AddEventListener<bool>(
                "cri_change",
                (flag) =>
                {
                    float temp = 1 - stats.Critical;
                    if (!flag)
                    {
                        stats.criticalStrikeMultiplier -= temp;
                    }
                    else
                    {
                        stats.criticalStrikeMultiplier += temp;
                    }
                }
            );
            MyEventSystem.Instance.EventTrigger<bool>("cri_change", true);
        }
        else if (id == 20047)
        {
            stats.CriticalStrikeMultiplier += (stats.MoveSpeed - 110) * 0.01f;
        }
        else if (id == 20048)
        {
            MyEventSystem.Instance.AddEventListener<bool>(
                "hp_change",
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
        }
        else if (id == 20049)
        {
            float coins = CoinsManager.Instance.Coins;
            stats.PowerOfAnode += coins;
            stats.PowerOfCathode += coins;
            void Fun()
            {
                stats.PowerOfAnode -= coins;
                stats.PowerOfCathode -= coins;
                MyEventSystem.Instance.RemoveEventListener("level_clear", Fun);
            }
            MyEventSystem.Instance.AddEventListener("level_clear", Fun);
        }
        else if (id == 20050)
        {
            stats.CathodeEnergy -= 50;
            stats.AnodeEnergy -= 50;
            stats.SwordCount += 2;
        }
        else if (id == 20052)
        {
            MyEventSystem.Instance.AddEventListener<EnemyBase>(
                "monsDie",
                (EnemyBase enemy) =>
                {
                    if (enemy.info.energyType == EnergyType.Cathode)
                    {
                        stats.CathodeEnergy += 1;
                    }
                }
            );
        }
        else if (id == 20053)
        {
            MyEventSystem.Instance.AddEventListener<EnemyBase>(
                "monsDie",
                (EnemyBase enemy) =>
                {
                    if (enemy.info.energyType == EnergyType.Anode)
                    {
                        stats.AnodeEnergy += 1;
                    }
                }
            );
        }
    }
}
