using System;
using UnityEngine;

public static class Utils
{
    public static Vector3 MouseWorldPos
    {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    }
}
