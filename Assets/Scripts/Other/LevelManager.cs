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

    /// <summary>
    /// 第几波
    /// </summary>
    public int wave = 0;
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
        DOTween.SetTweensCapacity(3000, 100);
    }

    private void Start()
    {
        StartNextLevel();
    }

    public bool t;

    private void FixedUpdate()
    {
        if (isStart)
        {
            if (timer > 0)
            {
                timer -= Time.fixedDeltaTime;
                string newstr = ((int)timer).ToString();
                if (newstr != text.text)
                {
                    text.transform.DOScale(1.25f, 0.2f)
                        .OnComplete(() =>
                        {
                            text.transform.DOScale(1f, 0.2f);
                        });
                }
                text.text = newstr;
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
        PlayerController.Instance.transform.position = Vector2.zero;
        PlayerController.Instance.CanControl = true;
        PlayerStats playerStats = PlayerController.Instance.playerStats;
        playerStats.curHealth.AddChange(
            playerStats.maxHealth.GetValue() - playerStats.curHealth.GetValue()
        );
        isStart = true;
        wave++;
        if (wave <= 3)
            timer = 20;
        else if (wave <= 10)
            timer = 30;
        else if (wave <= 15)
            timer = 40;
        else if (wave <= 19)
            timer = 50;
        else if (wave == 20)
            timer = 60;
        if (wave == 1)
        {
            StartSpawn(1, 2);
            StartSpawn(2, 2);
        }
        else if (wave == 2)
        {
            StartSpawn(1, 2);
            StartSpawn(2, 2);
            StartSpawn(7, 2);
            StartSpawn(8, 2);
        }
        else if (wave == 3)
        {
            StartSpawn(1, 2);
            StartSpawn(2, 2);
            StartSpawn(3, 3);
            StartSpawn(4, 3);
            StartSpawn(7, 2);
            StartSpawn(8, 2);
            StartSpawn(9, 3);
            StartSpawn(10, 3);
        }
        else if (wave == 4)
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
        else if (wave == 5)
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
        else if (wave == 6) { }
        else if (wave == 7) { }
        else if (wave == 8) { }
        else if (wave == 9) { }
        else if (wave == 10) { }
        else if (wave == 11) { }
        else if (wave == 12) { }
        else if (wave == 13) { }
    }

    public void LevelClear()
    {
        isStart = false;
        timer = 0;
        PlayerController.Instance.Idle();
        StopAllCoroutines();
        CancelInvoke();
        foreach (Transform monster in monstersParent)
        {
            monster.GetComponent<EnemyBase>().Die(false);
        }
        foreach (Transform bullet in bulletParent)
        {
            bullet
                .DOScale(0, 0.001f)
                .OnComplete(() =>
                {
                    Destroy(bullet.gameObject);
                });
        }
        CoinsManager.Instance.Clear();
        Invoke(nameof(ClearCallBack), 2f);
    }

    private void ClearCallBack()
    {
        UIManager.Instance.ShowShopUI();
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

    private void OnEnable()
    {
        MyEventSystem.Instance.AddEventListener<Vector2>("monsDie", DropCoin);
    }

    private void OnDisable()
    {
        MyEventSystem.Instance.RemoveEventListener<Vector2>("monsDie", DropCoin);
    }

    private void DropCoin(Vector2 pos)
    {
        if (wave <= 3)
        {
            CoinsManager.Instance.GenerateCoin(pos, 1, 0.5f);
        }
        else if (wave <= 10)
        {
            CoinsManager.Instance.GenerateCoin(pos, 1, 1f);
        }
        else if (wave <= 15)
        {
            CoinsManager.Instance.GenerateCoin(pos, 1, 1.5f);
        }
        else if (wave <= 20)
        {
            CoinsManager.Instance.GenerateCoin(pos, 1, 2f);
        }
    }
}
