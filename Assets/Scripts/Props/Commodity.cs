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
            stats.EnergyConsumption = 1;
        }
        if (id == 20039)
        {
            stats.AnodeEnergy += stats.CurHealth * 0.5f;
        }
        if (id == 20040)
        {
            stats.PowerOfAnode += stats.CurHealth * 0.08f;
        }
        if (id == 20041)
        {
            stats.CathodeEnergy += stats.RecoverForHealth * 20;
        }
        if (id == 20042)
        {
            stats.PowerOfCathode += stats.RecoverForHealth * 10;
        }
        if (id == 20043)
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
        if (id == 20044)
        {
            stats.propUpdate += () => { };
        }
    }
}
