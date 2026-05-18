using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 世界坐标飘字；使用对象池与按目标节流，减轻后期命中 UI 与 GC 压力。
/// Overdraw 仍可能较高：若 GPU 瓶颈可改为 TMP + 单 Canvas / 或减少同时飘字上限。
/// </summary>
public class WorldCanvas : MonoBehaviour
{
    private static WorldCanvas instance;
    public static WorldCanvas Instacne
    {
        get { return instance; }
    }

    [SerializeField]
    private GameObject messageUIprefab;

    [SerializeField]
    private int pooledWarmCount = 32;

    /// <summary>同一敌人实例最短飘字间隔（秒），穿透连段时降压。</summary>
    [SerializeField]
    private float minIntervalBetweenFloatsPerEnemy = 0.08f;

    private readonly Queue<GameObject> inactivePool = new Queue<GameObject>();

    private readonly Dictionary<int, float> nextEnemyFloatAllowedUnscaledTime = new Dictionary<int, float>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        PrewarmPool();
    }

    void PrewarmPool()
    {
        if (messageUIprefab == null)
            return;
        for (int i = 0; i < pooledWarmCount; i++)
        {
            GameObject go = Instantiate(messageUIprefab, transform);
            go.SetActive(false);
            inactivePool.Enqueue(go);
        }
    }

    /// <summary>兼容旧调用：池化飘字并返回 Text（用于链式改色等）。</summary>
    public Text ShowMessage(Vector2 pos, string message)
    {
        return ShowMessage(new Vector3(pos.x, pos.y, 0f), message);
    }

    public Text ShowMessage(Vector3 pos, string message)
    {
        Text text = RentText(pos, message);
        text.color = Color.white;
        PlayFloatTween(text.gameObject);
        return text;
    }

    public void UnregisterEnemyFloatThrottle(int enemyInstanceId)
    {
        if (enemyInstanceId != 0)
            nextEnemyFloatAllowedUnscaledTime.Remove(enemyInstanceId);
    }

    /// <summary>玩家受伤害等不参与按敌人节流。</summary>
    public void ShowPooledFloatingText(Vector3 worldPosition, string message, Color color)
    {
        Text text = RentText(worldPosition, message);
        text.color = color;
        PlayFloatTween(text.gameObject);
    }

    /// <summary>敌对命中飘字：同一敌人短时内合并显示以降低实例数。</summary>
    /// <returns>是否创建了飘字（被节流时为 false）</returns>
    public bool TryShowEnemyHitDamage(
        Vector3 worldPosition,
        string damageString,
        bool isCritColor,
        int enemyInstanceId
    )
    {
        float now = Time.unscaledTime;
        if (enemyInstanceId != 0 && minIntervalBetweenFloatsPerEnemy > 0f)
        {
            if (
                nextEnemyFloatAllowedUnscaledTime.TryGetValue(enemyInstanceId, out float allowAfter)
                && now < allowAfter
            )
            {
                return false;
            }

            nextEnemyFloatAllowedUnscaledTime[enemyInstanceId] = now + minIntervalBetweenFloatsPerEnemy;
        }

        Text text = RentText(worldPosition, damageString);
        text.color = isCritColor ? Color.yellow : Color.white;
        PlayFloatTween(text.gameObject);
        return true;
    }

    Text RentText(Vector3 worldPosition, string message)
    {
        GameObject uiobj =
            inactivePool.Count > 0 ? inactivePool.Dequeue() : Instantiate(messageUIprefab, transform);
        uiobj.SetActive(true);
        uiobj.transform.position = worldPosition;
        uiobj.transform.localScale = Vector3.zero;
        Text text = uiobj.GetComponent<Text>();
        text.text = message;
        return text;
    }

    void PlayFloatTween(GameObject uiobj)
    {
        Transform t = uiobj.transform;
        t.DOKill(false);
        float d = 1.25f;
        t.DOScale(0.010f, d / 4f);
        t.DOLocalMoveY(t.position.y + 1.1f, d)
            .OnComplete(() => ReturnFloaterToPool(uiobj));
    }

    void ReturnFloaterToPool(GameObject uiobj)
    {
        if (uiobj == null || instance != this)
            return;
        uiobj.transform.DOKill(false);
        uiobj.SetActive(false);
        inactivePool.Enqueue(uiobj);
    }
}
