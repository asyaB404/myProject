using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public Sprite baseMap_Victory;
    public Sprite baseMap_Fail;

    public GameObject BaseMap;

    public void SetBaseMap(bool isVictory)
    {
        if (isVictory)
        {
            BaseMap.GetComponent<SpriteRenderer>().sprite = baseMap_Victory;
        }
        else
        {
            BaseMap.GetComponent<SpriteRenderer>().sprite = baseMap_Fail;
        }
    }
}
