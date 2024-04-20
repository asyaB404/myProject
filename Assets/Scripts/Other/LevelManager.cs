using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get => instance;
    }
    public int level = 0;
    public float timer = 0;
    public bool isStart;
    public Transform monstersParent;
    public Transform bulletParent;
    public Text text;

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
        StartNextLevel();
    }

    public bool t;

    private void Update()
    {
        if (isStart)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                text.text = ((int)timer).ToString();
            }
            else
                LevelClear();
        }
        if (t)
        {
            for (int i = 0; i < 100; i++)
            {
                Vector2 pos = GetRandomPos();
                WorldCanvas.Instacne.ShowMessage(pos, pos.ToString());
            }
            t = false;
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
            StartSpawn(1, 2);
            StartSpawn(2, 2);
            StartSpawn(7, 2);
            StartSpawn(8, 2);
        }
        else if (level == 2)
        {
            StartSpawn(1, 1);
            StartSpawn(2, 1);
            StartSpawn(3, 3);
            StartSpawn(4, 3);
            StartSpawn(7, 1);
            StartSpawn(8, 1);
            StartSpawn(9, 3);
            StartSpawn(10, 3);
        }
        else if (level == 3)
        {
            StartSpawn(1, 0.6f);
            StartSpawn(2, 0.6f);
            StartSpawn(3, 3);
            StartSpawn(4, 3);
            StartSpawn(7, 0.6f);
            StartSpawn(8, 0.6f);
            StartSpawn(9, 3);
            StartSpawn(10, 3);
        }
        else if (level == 4)
        {
            StartSpawn(1, 1);
            StartSpawn(2, 1);
            StartSpawn(3, 2);
            StartSpawn(4, 2);
            StartSpawn(7, 1);
            StartSpawn(8, 1);
            StartSpawn(9, 2);
            StartSpawn(10, 2);
        }
        else if (level == 5)
        {
            StartSpawn(1, 0.5f);
            StartSpawn(2, 0.5f);
            StartSpawn(3, 1);
            StartSpawn(4, 1);
            StartSpawn(7, 1);
            StartSpawn(8, 1);
            StartSpawn(9, 2);
            StartSpawn(10, 2);
        }
    }

    public void LevelClear()
    {
        isStart = false;
        timer = 0;
        StopAllCoroutines();
        CancelInvoke();
        foreach (Transform monster in monstersParent)
        {
            monster
                .DOScale(0, 0.2f)
                .OnComplete(() =>
                {
                    Destroy(monster.gameObject);
                });
        }
        foreach (Transform bullet in bulletParent)
        {
            bullet
                .DOScale(0, 0.2f)
                .OnComplete(() =>
                {
                    Destroy(bullet.gameObject);
                });
        }
        CoinsManager.Instance.Clear();
    }

    private void StartSpawn(int id, float duration)
    {
        StartCoroutine(SpawnEnemyCoroutine(id, duration));
    }

    public IEnumerator SpawnEnemyCoroutine(int id, float duration)
    {
        while (isStart)
        {
            yield return new WaitForSeconds(duration);
            GameObject enemy = Instantiate(
                Resources.Load<GameObject>("Prefabs/Enemy/Monster" + id),
                monstersParent
            );
            enemy.transform.position = GetRandomPos();
        }
    }

    private Vector2 GetRandomPos()
    {
        int maxChoice = 20;
        Vector2 pos;
        pos = new(MyRandom.Instance.NextFloat(-11, 11), MyRandom.Instance.NextFloat(-11, 11));
        while (Physics2D.OverlapCircle(pos, 5.5f, 1 << 7) != null && maxChoice > 0)
        {
            pos = new(MyRandom.Instance.NextFloat(-11, 11), MyRandom.Instance.NextFloat(-11, 11));
            maxChoice--;
        }
        return pos;
    }
}
