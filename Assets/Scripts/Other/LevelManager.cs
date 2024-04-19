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
    public int level = 1;
    public float timer = 20;
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
