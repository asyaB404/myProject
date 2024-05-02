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
            stats.AnodeEnergy += stats.CurHealth * 0.5f;
        }
        else if (id == 20040)
        {
            stats.PowerOfAnode += stats.CurHealth * 0.08f;
        }
        else if (id == 20041)
        {
            stats.CathodeEnergy += stats.RecoverForHealth * 20;
        }
        else if (id == 20042)
        {
            stats.PowerOfCathode += stats.RecoverForHealth * 10;
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
                        stats.PowerOfAnode -= temp;
                        stats.PowerOfCathode -= temp;
                    }
                    else
                    {
                        stats.PowerOfAnode += temp;
                        stats.PowerOfCathode += temp;
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
                        stats.CriticalStrikeMultiplier -= temp;
                    }
                    else
                    {
                        stats.CriticalStrikeMultiplier += temp;
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
                        stats.CriticalStrikeMultiplier -= temp;
                    }
                    else
                    {
                        stats.CriticalStrikeMultiplier += temp;
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
                        stats.PowerOfAnode -= temp;
                        stats.PowerOfCathode -= temp;
                    }
                    else
                    {
                        stats.PowerOfAnode += temp;
                        stats.PowerOfCathode += temp;
                    }
                }
            );
            MyEventSystem.Instance.EventTrigger<bool>("hp_change", true);
        }
        else if (id == 20049)
        {
            MyEventSystem.Instance.AddEventListener<bool>(
                "coins_change",
                (flag) =>
                {
                    float temp = CoinsManager.Instance.Coins;
                    if (!flag)
                    {
                        stats.PowerOfAnode -= temp;
                        stats.PowerOfCathode -= temp;
                    }
                    else
                    {
                        stats.PowerOfAnode += temp;
                        stats.PowerOfCathode += temp;
                    }
                }
            );
            MyEventSystem.Instance.EventTrigger<bool>("coins_change", true);
        }
    }
}
