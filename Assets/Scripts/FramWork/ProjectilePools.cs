using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 玩家子弹与敌人子弹的简单对象池，减少 Instantiate/Destroy 与 GC。
/// </summary>
public static class ProjectilePools
{
    static GameObject _playerBulletPrefab;
    static Transform _bulletRoot;

    static readonly Stack<GameObject> PlayerBullets = new();

    static readonly Dictionary<GameObject, Stack<GameObject>> EnemyStacksByPrefab = new();

    public static void ConfigurePlayerBullet(GameObject prefab, Transform bulletParent)
    {
        _playerBulletPrefab = prefab;
        _bulletRoot = bulletParent;
    }

    static Stack<GameObject> GetEnemyStack(GameObject prefab)
    {
        if (!EnemyStacksByPrefab.TryGetValue(prefab, out var stack))
        {
            stack = new Stack<GameObject>();
            EnemyStacksByPrefab[prefab] = stack;
        }
        return stack;
    }

    public static PlayerBullet AcquirePlayerBullet(
        GameObject prefabFallback,
        Vector3 position,
        Quaternion rotation,
        Transform parentOverride = null
    )
    {
        GameObject prefab = _playerBulletPrefab != null ? _playerBulletPrefab : prefabFallback;
        Transform parent = parentOverride != null ? parentOverride : _bulletRoot;

        GameObject go;
        if (_playerBulletPrefab != null && PlayerBullets.Count > 0)
        {
            go = PlayerBullets.Pop();
            go.transform.SetParent(parent, worldPositionStays: false);
            go.transform.SetPositionAndRotation(position, rotation);
            go.SetActive(true);
            go.transform.DOKill(false);
        }
        else
        {
            go = Object.Instantiate(prefab, position, rotation, parent);
        }

        return go.GetComponent<PlayerBullet>();
    }

    public static void ReleasePlayerBullet(GameObject go)
    {
        if (go == null)
            return;
        if (_bulletRoot == null)
        {
            Object.Destroy(go);
            return;
        }
        var pb = go.GetComponent<PlayerBullet>();
        if (pb != null && pb.IsSwordOrbit)
            return;

        go.transform.DOKill(false);
        pb?.ResetRuntimeStateBeforePool();

        go.SetActive(false);
        go.transform.SetParent(_bulletRoot, worldPositionStays: false);
        PlayerBullets.Push(go);
    }

    public static GameObject AcquireEnemyBullet(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null)
            return null;

        if (_bulletRoot == null)
            return Object.Instantiate(prefab, position, rotation);

        Stack<GameObject> stack = GetEnemyStack(prefab);
        GameObject go;
        if (stack.Count > 0)
        {
            go = stack.Pop();
            go.transform.SetParent(_bulletRoot, worldPositionStays: false);
            go.transform.SetPositionAndRotation(position, rotation);
            go.SetActive(true);
            go.transform.DOKill(false);
        }
        else
        {
            go = Object.Instantiate(prefab, position, rotation, _bulletRoot);
            var ebNew = go.GetComponent<EnemyBullet>();
            if (ebNew != null)
                ebNew.AssignPoolPrefabKey(prefab);
        }

        var eb = go.GetComponent<EnemyBullet>();
        if (eb != null)
            eb.ResetRuntimeStateBeforePool();
        return go;
    }

    public static void ReleaseEnemyBullet(GameObject go, GameObject prefabKeyIfUnknown = null)
    {
        if (go == null)
            return;
        var eb = go.GetComponent<EnemyBullet>();
        if (eb != null && eb.EnemyBulletPrefabKey == null && prefabKeyIfUnknown != null)
            eb.AssignPoolPrefabKey(prefabKeyIfUnknown);
        GameObject key = eb != null && eb.EnemyBulletPrefabKey != null ? eb.EnemyBulletPrefabKey : prefabKeyIfUnknown;
        if (key == null || _bulletRoot == null)
        {
            Object.Destroy(go);
            return;
        }

        go.transform.DOKill(false);
        eb?.ResetRuntimeStateBeforePool();
        go.SetActive(false);
        go.transform.SetParent(_bulletRoot, worldPositionStays: false);

        Stack<GameObject> stack = GetEnemyStack(key);
        stack.Push(go);
    }

    public static void ReleaseAllBulletsUnder(Transform parent)
    {
        if (parent == null)
            return;

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            GameObject go = parent.GetChild(i).gameObject;
            go.transform.DOKill(false);

            var pb = go.GetComponent<PlayerBullet>();
            if (pb != null)
            {
                if (pb.IsSwordOrbit)
                {
                    Object.Destroy(go);
                }
                else
                {
                    ReleasePlayerBullet(go);
                }

                continue;
            }

            if (go.TryGetComponent<EnemyBullet>(out _))
            {
                ReleaseEnemyBullet(go);
                continue;
            }

            Object.Destroy(go);
        }
    }
}
