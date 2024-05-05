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
        // StartNextLevel(); // 注释，测试主界面
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
        UIManager.Instance.currentState = UIState.GamePlay;
        UIManager.Instance.UpdateBackGroundMusic(); // 暂且放在这里

        PlayerController.Instance.transform.position = Vector2.zero;
        PlayerController.Instance.CanControl = true;
        PlayerStats playerStats = PlayerController.Instance.playerStats;
        playerStats.CurHealth = playerStats.MaxHealth;
        isStart = true;
        wave++;
        if (wave <= 3)
            timer = 30;
        else if (wave <= 10)
            timer = 35;
        else if (wave <= 15)
            timer = 40;
        else if (wave <= 19)
            timer = 50;
        else
            timer = 60;
        if (wave == 1)
        {
            StartSpawn(1, 3);
            StartSpawn(2, 3);
        }
        else if (wave == 2)
        {
            StartSpawn(1, 5);
            StartSpawn(2, 5);
            StartSpawn(7, 5);
            StartSpawn(8, 5);
        }
        else if (wave == 3)
        {
            StartSpawn(1, 5);
            StartSpawn(2, 5);
            StartSpawn(3, 10);
            StartSpawn(4, 10);
            StartSpawn(7, 5);
            StartSpawn(8, 5);
            StartSpawn(9, 10);
            StartSpawn(10, 10);
        }
        else if (wave == 4)
        {
            StartSpawn(1, 6);
            StartSpawn(2, 6);
            StartSpawn(3, 6);
            StartSpawn(4, 6);
            StartSpawn(7, 6);
            StartSpawn(8, 6);
            StartSpawn(9, 6);
            StartSpawn(10, 6);
        }
        else if (wave == 5)
        {
            StartSpawn(1, 6);
            StartSpawn(2, 3);
            StartSpawn(3, 6);
            StartSpawn(4, 3);
            StartSpawn(7, 3);
            StartSpawn(8, 6);
            StartSpawn(9, 3);
            StartSpawn(10, 6);
        }
        else if (wave == 6)
        {
            StartSpawn(1, 3);
            StartSpawn(2, 6);
            StartSpawn(3, 3);
            StartSpawn(4, 6);
            StartSpawn(5, 5);
            StartSpawn(6, 5);
            StartSpawn(7, 6);
            StartSpawn(8, 3);
            StartSpawn(9, 6);
            StartSpawn(10, 3);
            StartSpawn(11, 5);
            StartSpawn(12, 5);
        }
        else if (wave == 7)
        {
            StartSpawn(3, 3);
            StartSpawn(4, 3);
            StartSpawn(5, 3);
            StartSpawn(6, 3);
            StartSpawn(9, 3);
            StartSpawn(10, 3);
            StartSpawn(11, 3);
            StartSpawn(12, 3);
        }
        else if (wave == 8)
        {
            StartSpawn(3, 3);
            StartSpawn(4, 3);
            StartSpawn(5, 2);
            StartSpawn(6, 2);
            StartSpawn(9, 3);
            StartSpawn(10, 3);
            StartSpawn(11, 2);
            StartSpawn(12, 2);
        }
        else if (wave == 9)
        {
            StartSpawn(3, 1);
            StartSpawn(4, 1);
            StartSpawn(5, 2);
            StartSpawn(6, 2);
            StartSpawn(9, 1);
            StartSpawn(10, 1);
            StartSpawn(11, 2);
            StartSpawn(12, 2);
        }
        else if (wave == 10)
        {
            StartSpawn(5, 1);
            StartSpawn(6, 1);
            StartSpawn(11, 1);
            StartSpawn(12, 1);
            StartSpawn(13, 3);
            StartSpawn(14, 3);
        }
        else if (wave == 11)
        {
            StartSpawn(5, 1);
            StartSpawn(6, 1);
            StartSpawn(11, 1);
            StartSpawn(12, 1);
            StartSpawn(13, 5);
            StartSpawn(14, 5);
            StartSpawn(19, 5);
            StartSpawn(20, 5);
        }
        else if (wave == 12)
        {
            StartSpawn(13, 5);
            StartSpawn(14, 5);
            StartSpawn(15, 10);
            StartSpawn(16, 10);
            StartSpawn(19, 5);
            StartSpawn(20, 5);
            StartSpawn(21, 10);
            StartSpawn(22, 10);
        }
        else if (wave == 13)
        {
            StartSpawn(13, 6);
            StartSpawn(14, 6);
            StartSpawn(15, 6);
            StartSpawn(16, 6);
            StartSpawn(19, 6);
            StartSpawn(20, 6);
            StartSpawn(21, 6);
            StartSpawn(22, 6);
        }
        else if (wave == 14)
        {
            StartSpawn(13, 6);
            StartSpawn(14, 3);
            StartSpawn(15, 6);
            StartSpawn(16, 3);
            StartSpawn(19, 3);
            StartSpawn(20, 6);
            StartSpawn(21, 3);
            StartSpawn(22, 6);
        }
        else if (wave == 15)
        {
            StartSpawn(13, 3);
            StartSpawn(14, 6);
            StartSpawn(15, 3);
            StartSpawn(16, 6);
            StartSpawn(17, 5);
            StartSpawn(18, 5);
            StartSpawn(19, 6);
            StartSpawn(20, 3);
            StartSpawn(21, 6);
            StartSpawn(22, 3);
            StartSpawn(23, 5);
            StartSpawn(24, 5);
        }
        else if (wave == 16)
        {
            StartSpawn(15, 3);
            StartSpawn(16, 3);
            StartSpawn(17, 3);
            StartSpawn(18, 3);
            StartSpawn(21, 3);
            StartSpawn(22, 3);
            StartSpawn(23, 3);
            StartSpawn(24, 3);
        }
        else if (wave == 17)
        {
            StartSpawn(15, 3);
            StartSpawn(16, 3);
            StartSpawn(17, 2);
            StartSpawn(18, 2);
            StartSpawn(21, 3);
            StartSpawn(22, 3);
            StartSpawn(23, 2);
            StartSpawn(24, 2);
        }
        else if (wave == 18)
        {
            StartSpawn(15, 1);
            StartSpawn(16, 1);
            StartSpawn(17, 2);
            StartSpawn(18, 2);
            StartSpawn(21, 1);
            StartSpawn(22, 1);
            StartSpawn(23, 2);
            StartSpawn(24, 2);
        }
        else if (wave == 19)
        {
            StartSpawn(17, 1);
            StartSpawn(18, 1);
            StartSpawn(23, 1);
            StartSpawn(24, 1);
        }
        else if (wave == 20)
        {
            StartSpawn(17, 1);
            StartSpawn(18, 0.5f);
            StartSpawn(23, 0.5f);
            StartSpawn(24, 1);
        }
    }

    public void LevelClear()
    {
        isStart = false;
        timer = 0;
        PlayerController.Instance.Idle();
        StopAllCoroutines();
        CancelInvoke();
        MyEventSystem.Instance.EventTrigger("level_clear");
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
        PlayerController.Instance.playerStats.CurHealth = PlayerController
            .Instance
            .playerStats
            .MaxHealth;
        UIManager.Instance.ShowShopUI();
    }

    public void StartSpawn(int id, float duration)
    {
        StartCoroutine(SpawnEnemyCoroutine(id, duration));
    }

    private IEnumerator SpawnEnemyCoroutine(int id, float duration)
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
        MyEventSystem.Instance.AddEventListener<EnemyBase>("monsDie", DropCoin);
    }

    private void OnDisable()
    {
        MyEventSystem.Instance.RemoveEventListener<EnemyBase>("monsDie", DropCoin);
    }

    private void DropCoin(EnemyBase enemy)
    {
        if (wave <= 16)
        {
            CoinsManager.Instance.GenerateCoin(enemy.transform.position, 1, 1);
        }
        else if (wave <= 20)
        {
            CoinsManager.Instance.GenerateCoin(enemy.transform.position, 1, 2);
        }
    }
}
