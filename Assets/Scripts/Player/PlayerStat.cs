using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    [SerializeField]
    private float val;
    public List<float> changes;

    public float GetValue()
    {
        float res = val;
        foreach (float c in changes)
            res += c;
        return res > 0 ? res : 0;
    }

    public void AddChange(float change)
    {
        if (change != 0)
        {
            changes.Add(change);
        }
    }

    public void Clear()
    {
        changes.Clear();
    }
}
