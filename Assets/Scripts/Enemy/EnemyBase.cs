using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnergyType
{
    Anode,
    Cathode
}

public class EnemyBase : MonoBehaviour
{
    [Header("基本属性")]
    public float curHealth;
    public EnemyInfo info;
    public Vector2 DirectionToPlayer
    {
        get
        {
            return (PlayerController.Instance.transform.position - transform.position).normalized;
        }
    }

    [Header("组件")]
    public Rigidbody2D rb;
    public Animator anim;
    public EnemyStateMachine stateMachine;
    public SpriteRenderer sr;

    public virtual void Start()
    {
        curHealth = info.health;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (info.energyType == EnergyType.Anode)
                playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
            else
                playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
            playerStats.TakeDamage(Mathf.FloorToInt(info.atkMul));
        }
    }

    public virtual void Update()
    {
        stateMachine?.CurState?.OnUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth < 0)
            Die();
    }

    public virtual void Die() { }

    private void OnDestroy()
    {
        stateMachine?.CurState?.OnExit();
    }
}
