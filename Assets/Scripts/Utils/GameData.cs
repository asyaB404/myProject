using UnityEngine;

public class GameData
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    public static float GlobalMoveSpeed = 5 / 100f;
    public static float GlobalRange = 12 / 100f;
    public static float GlobalBulletFlyTime = 1 / 100f;

    /// <summary>
    /// 由 PlayerController 在每帧逻辑更新末尾写入；敌兵 AI / 朝向用此处避免频繁访问 Player Transform。
    /// </summary>
    public static Vector2 CachedPlayerPosition;
}
