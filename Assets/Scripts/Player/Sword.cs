using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject swordPrefab;

    public void Refresh()
    {
        int size = PlayerController.Instance.playerStats.SwordCount;
        float duration = 360 / size;
        float temp = 0;
        for (int i = 0; i < PlayerController.Instance.playerStats.SwordCount; i++)
        {
            GameObject sword = Instantiate(swordPrefab);
            PlayerBullet swordcomp = sword.GetComponent<PlayerBullet>();
            if (i % 2 == 0)
                swordcomp.InitForSword(EnergyType.Anode);
            else
                swordcomp.InitForSword(EnergyType.Cathode);

            sword.transform.localEulerAngles = new(0, 0, duration);
            temp += duration;
        }
    }
}
