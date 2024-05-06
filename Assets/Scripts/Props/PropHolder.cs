using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHolder : MonoBehaviour
{
    public GameSetting gameSetting;
    public PropBase curProp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            curProp.EffectAfterGet(player.playerStats, gameSetting);
        }
    }
}
