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
        if (id == 20038)
        {
            stats.energyConsumption.Set(1);
        }
        if (id == 20039)
        {
            stats.anodeEnergy.AddChange(stats.curHealth.GetValue() * 0.5f);
        }
        if (id == 20040)
        {
            stats.powerOfAnode.AddChange(stats.curHealth.GetValue() * 0.08f);
        }
        if (id == 20041)
        {
            stats.cathodeEnergy.AddChange(stats.recoverForHealth.GetValue() * 20);
        }
        if (id == 20042)
        {
            stats.powerOfCathode.AddChange(stats.recoverForHealth.GetValue() * 10);
        }
        if (id == 20043)
        {
            if (stats.cathodeEnergy.GetValue() > stats.anodeEnergy.GetValue())
            {
                stats.anodeEnergy.Set(stats.cathodeEnergy.GetValue());
            }
            else
            {
                stats.cathodeEnergy.Set(stats.anodeEnergy.GetValue());
            }
        }
        if (id == 20044) { }
    }
}
