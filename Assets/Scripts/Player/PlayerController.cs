using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats playerStats;



    public GameObject bullet;

    public bool shootMode;
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void Shoot()
    {
        if (!shootMode)
        {
            ShootMode1();
        }
        else
        {
            ShootMode2();;
        } 
    }

    public void ShootMode1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet,transform.position,Quaternion.identity);
            BulletBase script = newBullet.GetComponent<BulletBase>();
            script.SetupBullet(Mathf.RoundToInt(playerStats.piercingAttack.GetValue()), energyType.Cathode, Mathf.RoundToInt(playerStats.powerOfCathode.GetValue()));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletBase script = newBullet.GetComponent<BulletBase>();
            script.SetupBullet(Mathf.RoundToInt(playerStats.piercingAttack.GetValue()), energyType.Anode, Mathf.RoundToInt(playerStats.powerOfAnode.GetValue()));
        }
    }

    public void ShootMode2()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletBase script = newBullet.GetComponent<BulletBase>();
            script.SetupBullet(Mathf.RoundToInt(playerStats.piercingAttack.GetValue()), energyType.Cathode, Mathf.RoundToInt(playerStats.powerOfCathode.GetValue()));
        }
        else if (Input.GetMouseButton(1))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletBase script = newBullet.GetComponent<BulletBase>();
            script.SetupBullet(Mathf.RoundToInt(playerStats.piercingAttack.GetValue()), energyType.Anode, Mathf.RoundToInt(playerStats.powerOfAnode.GetValue()));
        }
    }
}
