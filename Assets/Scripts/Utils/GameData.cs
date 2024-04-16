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
    
    private int playersCoins;
    public int PlayersCoins
    {
        get { return playersCoins; }
        set
        {
            playersCoins = value;
            if (playersCoins < 0)
            {
                playersCoins = 0;
            }
        }
    }

    private int tempCoins;
    public int TempCoins
    {
        get { return tempCoins; }
        set
        {
            tempCoins = value;
            if (tempCoins < 0)
            {
                tempCoins = 0;
            }
        }
    }

    public int level;
}
