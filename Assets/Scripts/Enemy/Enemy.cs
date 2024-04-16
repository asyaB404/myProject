using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnergyType
{
    Anode,
    Cathode
}

public class Enemy : MonoBehaviour
{
    [Header("��������")]
    public EnergyType type;
    public float energy;
    public float maxHealth;
    public float curHealth;
    public int attackMultiple;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
    public float alertRange;
    public float effectiveRange;

    [Header("���")]
    public Rigidbody2D rb;
    public Animator anim;
    public EnemyStateMachine stateMachine;
    public SpriteRenderer sr;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine();
        sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (stateMachine.curState != null)
            stateMachine.curState.OnUpdate();
    }

    public virtual void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth < 0)
            Die();
    }

    public virtual void Die() { }
}
