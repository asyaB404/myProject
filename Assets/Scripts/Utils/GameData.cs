using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }
    
    public static float GlobalMoveSpeed = 0.04f;
}
