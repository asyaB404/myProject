using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private static Sword instance;
    public static Sword Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(instance.gameObject);
        }
        instance = this;
    }

    public GameObject swordPrefab;

    private void Update()
    {
        transform.Rotate(0, 0, 720 * Time.deltaTime * PlayerController.Instance.facingRight);
    }

    [ContextMenu("test")]
    public void Refresh()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject, 0.5f);
        }
        int size = PlayerController.Instance.playerStats.SwordCount;
        float duration = 0;
        if (size != 0)
            duration = 360 / size;

        float temp = 0;
        for (int i = 0; i < PlayerController.Instance.playerStats.SwordCount; i++)
        {
            GameObject sword = Instantiate(swordPrefab, transform);
            PlayerBullet swordcomp = sword.GetComponentInChildren<PlayerBullet>(true);
            if (i % 2 == 0)
                swordcomp.InitForSword(EnergyType.Anode);
            else
                swordcomp.InitForSword(EnergyType.Cathode);

            sword.transform.localEulerAngles = new(0, 0, temp);
            temp += duration;
        }
    }
}
