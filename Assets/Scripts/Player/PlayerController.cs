using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance; }
    }
    public PlayerStats playerStats;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rb;
    public bool canControl;
    public int facingRight = 1;
    public CameraPos cameraPos;
    public Sword sword;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        playerStats = GetComponent<PlayerStats>();
    }

    public void Filp()
    {
        facingRight = -facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void AutoFilp()
    {
        if ((Utils.MouseWorldPos - transform.position).x * facingRight < 0)
            Filp();
    }

    private void Update()
    {
        if (canControl && Time.timeScale != 0)
        {
            AutoFilp();
            UpdateMove();
            UpdateHeal();
            UpdateShoot();
        }
    }

    [SerializeField]
    private Vector2 moveInput;

    private void UpdateMove()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        animator.SetBool("isRun", moveInput != Vector2.zero);
        float speed = playerStats.moveSpeed.GetValue();
        if (speed < 20)
        {
            speed = 20;
        }
        // transform.Translate(moveInput * Time.deltaTime * speed * GameData.GlobalMoveSpeed);
        // Vector2 moveAmount = moveInput * Time.deltaTime * speed * GameData.GlobalMoveSpeed;
        // rb.MovePosition(rb.position + moveAmount);
        rb.velocity = speed * GameData.GlobalMoveSpeed * moveInput;
    }

    //0-1
    [SerializeField]
    private float shootCD;

    private void UpdateShoot()
    {
        if (shootCD > 0)
        {
            shootCD -= Time.deltaTime * playerStats.moveSpeed.GetValue() / 100;
            return;
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            shootCD = 1;
            float scat = playerStats.attackScattering.GetValue();
            UnityAction<Vector3> shootFun = null;
            if (
                Input.GetMouseButton(0)
                && playerStats.cathodeEnergy.GetValue() >= playerStats.energyConsumption.GetValue()
            )
            {
                shootFun = Shoot0;
                playerStats.cathodeEnergy.AddChange(-playerStats.energyConsumption.GetValue());
            }
            else if (
                Input.GetMouseButton(1)
                && playerStats.anodeEnergy.GetValue() >= playerStats.energyConsumption.GetValue()
            )
            {
                shootFun = Shoot1;
                playerStats.anodeEnergy.AddChange(-playerStats.energyConsumption.GetValue());
            }

            if (shootFun != null)
            {
                Vector3 rotation = sword.transform.rotation.eulerAngles;
                float degree = 0;
                //配置散射个数的总体角度
                if (scat == 2)
                    degree = 10;
                else if (scat == 3)
                    degree = 14;
                else if (scat == 4)
                    degree = 20;
                else if (scat == 5)
                    degree = 25;
                else if (scat == 6)
                    degree = 30;
                else if (scat == 7)
                    degree = 35;
                else if (scat == 8)
                    degree = 40;
                else if (scat >= 45)
                    degree = 45;
                rotation.z += degree / 2;
                shootFun.Invoke(rotation);
                for (int i = 0; i < scat - 1; i++)
                {
                    rotation.z -= degree / (scat - 1);
                    shootFun.Invoke(rotation);
                }
            }
        }
    }

    private void Shoot0(Vector3 rotaion)
    {
        GameObject newBullet = Instantiate(
            bullet,
            sword.firePoint.position,
            Quaternion.Euler(rotaion)
        );
        BulletBase script = newBullet.GetComponent<BulletBase>();
        script.SetupBullet(
            Mathf.RoundToInt(playerStats.piercingAttack.GetValue()),
            EnergyType.Cathode,
            Mathf.RoundToInt(playerStats.powerOfCathode.GetValue())
        );
    }

    private void Shoot1(Vector3 rotaion)
    {
        GameObject newBullet = Instantiate(
            bullet,
            sword.firePoint.position,
            Quaternion.Euler(rotaion)
        );
        BulletBase script = newBullet.GetComponent<BulletBase>();
        script.SetupBullet(
            Mathf.RoundToInt(playerStats.piercingAttack.GetValue()),
            EnergyType.Anode,
            Mathf.RoundToInt(playerStats.powerOfAnode.GetValue())
        );
    }

    //0-1
    private float healCD = 1f;

    private void UpdateHeal()
    {
        if (
            healCD > 0
            && playerStats.curHealth.GetValue() < playerStats.maxHealth.GetValue()
            && playerStats.recoverForHealth.GetValue() > 0
        )
        {
            healCD -= Time.deltaTime * playerStats.recoverForHealth.GetValue();
        }
        else if (healCD < 0)
        {
            playerStats.curHealth.AddChange(1);
            healCD = 1;
            if (playerStats.curHealth.GetValue() > playerStats.maxHealth.GetValue())
            {
                playerStats.curHealth.AddChange(
                    playerStats.maxHealth.GetValue() - playerStats.curHealth.GetValue()
                );
            }
        }
    }
}
