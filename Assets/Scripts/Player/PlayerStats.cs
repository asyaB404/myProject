using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sr;

    [Header("主要属性")]
    public PlayerStat maxHealth;
    public PlayerStat curHealth;
    public PlayerStat moveSpeed;
    public PlayerStat recoverForHealth;
    public PlayerStat Defence;
    public PlayerStat Critical;

    [Header("次要属性")]
    public PlayerStat anodeEnergy; //阳
    public PlayerStat cathodeEnergy; //阴
    public PlayerStat powerOfCathode;
    public PlayerStat powerOfAnode;
    public PlayerStat criticalStrikeMultiplier; //暴击倍率
    public PlayerStat attackScattering; //散射
    public PlayerStat energyConsumption; //能量消耗
    public PlayerStat piercingAttack; //穿透

    public float invCD = 0.5f; //无敌帧
    private float invTimer;
    private bool invFlag;

    public void TakeDamage(int attackMultiple)
    {
        if (invTimer <= 0)
        {
            StartCoroutine(nameof(DamagedCoroutine));
            invTimer = invCD;
            int damage =
                Mathf.Abs(
                    Mathf.RoundToInt(anodeEnergy.GetValue())
                        - Mathf.RoundToInt(cathodeEnergy.GetValue())
                ) * attackMultiple;
            damage =
                damage - Mathf.RoundToInt(Defence.GetValue()) > 0
                    ? damage - Mathf.RoundToInt(Defence.GetValue())
                    : 0;
            curHealth.AddChange(-damage);
            if (curHealth.GetValue() < 0)
                Die();
        }
    }

    IEnumerator DamagedCoroutine()
    {
        InvokeRepeating(nameof(DamagedEffect), 0, 0.1f);
        yield return new WaitForSeconds(invCD);
        CancelInvoke();
        sr.color = Color.white;
    }

    private void DamagedEffect()
    {
        sr.color = invFlag ? Color.white : Color.clear;
        invFlag = !invFlag;
    }

    private void Update()
    {
        if (invTimer > 0)
        {
            invTimer -= Time.deltaTime;
        }
    }

    public void Die()
    {
        StopAllCoroutines();
        CancelInvoke();
    }
}
