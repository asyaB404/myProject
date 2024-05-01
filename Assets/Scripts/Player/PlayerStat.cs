using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    [SerializeField]
    private float val;

    public float GetValue()
    {
        if (val < 0)
        {
            return 0;
        }
        return val;
    }

    public void AddChange(float change)
    {
        if (change != 0)
        {
            val += change;
        }
    }
}
