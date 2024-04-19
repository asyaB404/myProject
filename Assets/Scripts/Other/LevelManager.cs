using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelManager instance;
    public LevelManager Instance
    {
        get => instance;
    }
    public int level = 0;
    public float timer = 0;
    public bool isStart = true;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        MyEventSystem.Instance.AddEventListener<Vector2>(
            "monsDie",
            (Vector2 pos) =>
            {
                if (level <= 3)
                {
                    CoinsManager.Instance.GenerateCoin(pos, 1, 0.5f);
                }
                else if (level <= 10)
                {
                    CoinsManager.Instance.GenerateCoin(pos, 1, 1f);
                }
                else if (level <= 15)
                {
                    CoinsManager.Instance.GenerateCoin(pos, 1, 1.5f);
                }
                else if (level <= 20)
                {
                    CoinsManager.Instance.GenerateCoin(pos, 1, 2f);
                }
            }
        );
    }

    private void Start()
    {
        // StartNextLevel();
    }

    private void Update()
    {
        if (isStart)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                LevelClear();
            }
        }
    }

    public void StartNextLevel()
    {
        isStart = true;
        level++;
        if (level <= 3)
            timer = 20;
        else if (level <= 10)
            timer = 30;
        else if (level <= 15)
            timer = 40;
        else if (level <= 19)
            timer = 50;
        else if (level == 20)
            timer = 60;
        if (level == 1)
        {
            SpawnEnemyCoroutine(1, 2);
            SpawnEnemyCoroutine(2, 2);
            SpawnEnemyCoroutine(7, 2);
            SpawnEnemyCoroutine(8, 2);
        }
        else if (level == 2)
        {
            SpawnEnemyCoroutine(1, 1);
            SpawnEnemyCoroutine(2, 1);
            SpawnEnemyCoroutine(3, 3);
            SpawnEnemyCoroutine(4, 3);
            SpawnEnemyCoroutine(7, 1);
            SpawnEnemyCoroutine(8, 1);
            SpawnEnemyCoroutine(9, 3);
            SpawnEnemyCoroutine(10, 3);
        }
        else if (level == 3)
        {
            SpawnEnemyCoroutine(1, 0.6f);
            SpawnEnemyCoroutine(2, 0.6f);
            SpawnEnemyCoroutine(3, 3);
            SpawnEnemyCoroutine(4, 3);
            SpawnEnemyCoroutine(7, 0.6f);
            SpawnEnemyCoroutine(8, 0.6f);
            SpawnEnemyCoroutine(9, 3);
            SpawnEnemyCoroutine(10, 3);
        }
        else if (level == 4)
        {
            SpawnEnemyCoroutine(1, 1);
            SpawnEnemyCoroutine(2, 1);
            SpawnEnemyCoroutine(3, 2);
            SpawnEnemyCoroutine(4, 2);
            SpawnEnemyCoroutine(7, 1);
            SpawnEnemyCoroutine(8, 1);
            SpawnEnemyCoroutine(9, 2);
            SpawnEnemyCoroutine(10, 2);
        }
        else if (level == 5)
        {
            SpawnEnemyCoroutine(1, 0.5f);
            SpawnEnemyCoroutine(2, 0.5f);
            SpawnEnemyCoroutine(3, 1);
            SpawnEnemyCoroutine(4, 1);
            SpawnEnemyCoroutine(7, 1);
            SpawnEnemyCoroutine(8, 1);
            SpawnEnemyCoroutine(9, 2);
            SpawnEnemyCoroutine(10, 2);
        }
    }

    public void LevelClear()
    {
        isStart = false;
        timer = 0;
        StopAllCoroutines();
        CancelInvoke();
        CoinsManager.Instance.Clear();
    }

    IEnumerable SpawnEnemyCoroutine(int id, float duration)
    {
        yield return new WaitForSeconds(duration);
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Enemy" + id));
    }
}
