using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [ContextMenuItem("test", nameof(Test1))]
    public Commodity commodity;
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void Test1()
    {
        commodity.EffectAfterGet(playerStats);
    }
}
