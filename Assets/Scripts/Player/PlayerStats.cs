using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private GameSetting gameSetting;

    [Header("主要属性")]
    [SerializeField]
    private float maxHealth;
    public float MaxHealth
    {
        get
        {
            if (maxHealth < 0)
                return 0;
            return maxHealth;
        }
        set { maxHealth = value; }
    }

    [SerializeField]
    public float curHealth;
    public float CurHealth
    {
        get
        {
            if (curHealth < 0)
                return 0;
            return curHealth;
        }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("hp_change", false);
            curHealth = value;
            MyEventSystem.Instance.EventTrigger<bool>("hp_change", true);
        }
    }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed
    {
        get
        {
            if (moveSpeed < 0)
                return 0;
            return moveSpeed;
        }
        set { moveSpeed = value; }
    }

    [SerializeField]
    private float recoverForHealth;
    public float RecoverForHealth
    {
        get
        {
            if (recoverForHealth < 0)
                return 0;
            return recoverForHealth;
        }
        set { recoverForHealth = value; }
    }

    [SerializeField]
    private float defence;
    public float Defence
    {
        get
        {
            if (defence < 0)
                return 0;
            return defence;
        }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("def_change", false);
            defence = value;
            MyEventSystem.Instance.EventTrigger<bool>("def_change", true);
        }
    }

    [SerializeField]
    private float critical;
    public float Critical
    {
        get
        {
            if (critical < 0)
                return 0;
            return critical;
        }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("cri_change", false);
            critical = value;
            MyEventSystem.Instance.EventTrigger<bool>("cri_change", true);
        }
    }

    [Header("次要属性")]
    [SerializeField]
    private float anodeEnergy;
    public float AnodeEnergy
    {
        get
        {
            if (anodeEnergy < 0)
                return 0;
            return anodeEnergy;
        }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("energy_change", false);
            anodeEnergy = value;
            MyEventSystem.Instance.EventTrigger<bool>("energy_change", true);
        }
    }

    [SerializeField]
    private float cathodeEnergy;
    public float CathodeEnergy
    {
        get
        {
            if (cathodeEnergy < 0)
                return 0;
            return cathodeEnergy;
        }
        set
        {
            MyEventSystem.Instance.EventTrigger<bool>("energy_change", false);
            cathodeEnergy = value;
            MyEventSystem.Instance.EventTrigger<bool>("energy_change", true);
        }
    }

    [SerializeField]
    public float powerOfCathode;
    public float PowerOfCathode
    {
        get
        {
            if (powerOfCathode < 1)
                return 1;
            return powerOfCathode;
        }
        set { powerOfCathode = value; }
    }

    [SerializeField]
    public float powerOfAnode;
    public float PowerOfAnode
    {
        get
        {
            if (powerOfAnode < 1)
                return 1;
            return powerOfAnode;
        }
        set { powerOfAnode = value; }
    }

    [SerializeField]
    public float criticalStrikeMultiplier;
    public float CriticalStrikeMultiplier
    {
        get
        {
            if (criticalStrikeMultiplier < 0)
                return 0;
            return criticalStrikeMultiplier;
        }
        set { criticalStrikeMultiplier = value; }
    }

    [SerializeField]
    private float attackScattering;
    public float AttackScattering
    {
        get
        {
            if (attackScattering < 0)
                return 0;
            return attackScattering;
        }
        set { attackScattering = value; }
    }

    [SerializeField]
    private float energyConsumption;
    public float EnergyConsumption
    {
        get
        {
            if (energyConsumption < 0)
                return 0;
            return energyConsumption;
        }
        set { energyConsumption = value; }
    }

    [SerializeField]
    private float piercingAttack;
    public float PiercingAttack
    {
        get
        {
            if (piercingAttack < 0)
                return 0;
            return piercingAttack;
        }
        set { piercingAttack = value; }
    }

    [SerializeField]
    private int swordCount;
    public int SwordCount
    {
        get { return swordCount; }
        set
        {
            swordCount = value;
            Sword.Instance.Refresh();
        }
    }

    public float invCD = 0.25f; //无敌帧
    public bool IsInv
    {
        get { return invTimer > 0; }
    }
    private float invTimer;

    public float pullingRange = 1.5f;
    public bool isOpenPullingCoins = true;

    public void TakeDamage(float attackMultiple)
    {
        if (invTimer <= 0)
        {
            invTimer = invCD;
            float damage =
                Mathf.Abs(Mathf.RoundToInt(AnodeEnergy) - Mathf.RoundToInt(CathodeEnergy))
                * attackMultiple;
            damage =
                damage - Mathf.RoundToInt(Defence) > 0 ? damage - Mathf.RoundToInt(Defence) : 0;
            if (damage >= 1)
                WorldCanvas
                    .Instacne.ShowMessage(transform.position, Mathf.FloorToInt(damage).ToString())
                    .color = Color.red;
            CurHealth -= damage;
            if (CurHealth <= 0)
                Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullingRange);
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        float x = pos.x;
        float y = pos.y;
        if (x < -12)
        {
            Debug.Log("1");
            transform.position += Vector3.right * (-11.5f - x);
        }
        else if (x > 12)
        {
            Debug.Log("2");
            transform.position += Vector3.right * (11.5f - x);
        }
        if (y < -12)
        {
            Debug.Log("1");
            transform.position += Vector3.up * (-11.5f - y);
        }
        else if (y > 12)
        {
            Debug.Log("2");
            transform.position += Vector3.up * (11.5f - y);
        }
        if (invTimer > 0)
        {
            invTimer -= Time.deltaTime;
        }
        if (isOpenPullingCoins)
        {
            Collider2D[] objs = Physics2D.OverlapCircleAll(
                transform.position,
                pullingRange,
                1 << 6
            );
            foreach (var obj in objs)
            {
                Vector2 direction = (transform.position - obj.transform.position).normalized;
                float len = (transform.position - obj.transform.position).magnitude;
                obj.transform.Translate(direction * Time.deltaTime * (len + 20));
            }
        }
    }

    [ContextMenu("die")]
    public void Die()
    {
        StopAllCoroutines();
        CancelInvoke();
        Debug.Log("Die");
        UIManager.Instance.ShowGameOverUI();
        MyEventSystem.Instance.Clear();
    }

    public void LoadGameSetting()
    {
        maxHealth = gameSetting.default_maxHeath + gameSetting.addition_maxHeath;
        moveSpeed = gameSetting.default_moveSpeed + gameSetting.addition_moveSpeed;
        recoverForHealth =
            gameSetting.default_recoverForHealth + gameSetting.addition_recoverForHealth;
        defence = gameSetting.default_defense + gameSetting.addition_defense;
        critical = gameSetting.default_critial + gameSetting.addition_critial;
        anodeEnergy = gameSetting.default_anodeEnergy + gameSetting.addition_anodeEnergy;
        cathodeEnergy = gameSetting.default_cathodeEnergy + gameSetting.addition_cathodeEnergy;
        powerOfAnode = gameSetting.default_powerOfAnode + gameSetting.addition_powerOfAnode;
        powerOfCathode = gameSetting.default_powerOfCathode + gameSetting.addition_powerOfCathode;
        criticalStrikeMultiplier =
            gameSetting.default_criticalStrikeMultipiler
            + gameSetting.addition_criticalStrikeMultipiler;
        attackScattering = gameSetting.default_attackScatter + gameSetting.addition_attackScatter;
        energyConsumption =
            gameSetting.default_energyConsumption + gameSetting.addition_energyConsumption;
        piercingAttack = gameSetting.default_piercingAttack + gameSetting.addition_piercingAttack;
    }
}
