using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "怪物")]
public class EnemyInfo : ScriptableObject
{
    public int id;
    public string itemName;

    /// <summary>
    /// 近=1，远=2，boss=3
    /// </summary>
    public int enemyType;
    public EnergyType energyType;

    [SerializeField]
    private float health;
    public float Health
    {
        get
        {
            int wave = LevelManager.Instance.wave;
            if (wave <= 20)
                return health;
            return health * (wave - 20);
        }
    }

    /// <summary>
    /// 命中增加主角极性能量
    /// </summary>
    [SerializeField]
    private float recoverFromAtk;
    public float RecoverFromAtk
    {
        get
        {
            int wave = LevelManager.Instance.wave;
            if (wave <= 20)
                return recoverFromAtk;
            return recoverFromAtk * (wave - 20);
        }
    }

    [SerializeField]
    private float atkMul;
    public float AtkMul
    {
        get
        {
            int wave = LevelManager.Instance.wave;
            if (wave <= 20)
                return atkMul;
            return atkMul + (wave - 20) * 0.1f;
        }
    }
    public float speed;
    public float atkSpeed;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float range;

    /// <summary>
    /// 子弹范围....其实从实现上已经把他当成飞行时间了(因为子弹速度相等)
    /// </summary>
    public float bulletRange;
}
