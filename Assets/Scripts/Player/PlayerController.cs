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

    [SerializeField]
    private bool canControl;
    public bool CanControl
    {
        get => canControl;
        set
        {
            canControl = value;
            cameraPos.isOpen = value;
        }
    }
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
        if (CanControl && Time.timeScale != 0)
        {
            AutoFilp();
            UpdateMove();
            UpdateHeal();
            UpdateShoot();
        }
    }

    [SerializeField]
    private Vector2 moveInput;

    public void Idle()
    {
        CanControl = false;
        rb.velocity = Vector2.zero;
        animator.SetBool("isRun", false);
    }

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
    private float shootCD1;

    [SerializeField]
    private float shootCD2;

    private void UpdateShoot()
    {
        if (shootCD1 > 0 || shootCD2 > 0)
        {
            shootCD1 -= Time.deltaTime * playerStats.moveSpeed.GetValue() * 3 / 100;
            shootCD2 -= Time.deltaTime * playerStats.moveSpeed.GetValue() * 3 / 100;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            float scat = playerStats.attackScattering.GetValue();
            if (
                Input.GetMouseButton(1)
                && playerStats.cathodeEnergy.GetValue() >= playerStats.energyConsumption.GetValue()
                && shootCD1 <= 0
            )
            {
                shootCD1 = 1;
                Vector3 rotation = sword.transform.rotation.eulerAngles;
                float degree = 0;
                if (2 <= scat && scat <= 9)
                    degree = scat * 5;
                else if (scat >= 10)
                    degree = 45;

                rotation.z += degree / 2;
                Shoot0(rotation);
                for (int i = 0; i < scat - 1; i++)
                {
                    rotation.z -= degree / (scat - 1);
                    Shoot0(rotation);
                }
                playerStats.cathodeEnergy.AddChange(-playerStats.energyConsumption.GetValue());
            }
            if (
                Input.GetMouseButton(0)
                && playerStats.anodeEnergy.GetValue() >= playerStats.energyConsumption.GetValue()
                && shootCD2 <= 0
            )
            {
                shootCD2 = 1;
                Vector3 rotation = sword.transform.rotation.eulerAngles;
                float degree = 0;
                if (2 <= scat && scat <= 9)
                    degree = scat * 5;
                else if (scat >= 10)
                    degree = 45;

                rotation.z += degree / 2;
                Shoot1(rotation);
                for (int i = 0; i < scat - 1; i++)
                {
                    rotation.z -= degree / (scat - 1);
                    Shoot1(rotation);
                }
                playerStats.anodeEnergy.AddChange(-playerStats.energyConsumption.GetValue());
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
        PlayerBullet script = newBullet.GetComponent<PlayerBullet>();
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
        PlayerBullet script = newBullet.GetComponent<PlayerBullet>();
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
