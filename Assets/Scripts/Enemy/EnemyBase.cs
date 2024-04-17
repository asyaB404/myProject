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
    [Header("��������")]
    public EnergyType type;
    public float curHealth;
    public EnemyInfo info;
    public Vector2 DirectionToPlayer
    {
        get
        {
            return (PlayerController.Instance.transform.position - transform.position).normalized;
        }
    }

    [Header("���")]
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
