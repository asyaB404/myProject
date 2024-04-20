using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public int facingRight = 1;
    public bool autoFilp = true;

    public virtual void Start()
    {
        curHealth = info.health;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats.IsInv)
            {
                if (info.energyType == EnergyType.Anode)
                    playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
                else
                    playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
                playerStats.TakeDamage(Mathf.FloorToInt(info.atkMul));
            }
        }
    }

    public void Filp()
    {
        facingRight = -facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void AutoFilp()
    {
        if (DirectionToPlayer.x > 0 && facingRight == -1)
        {
            Filp();
        }
        else if (DirectionToPlayer.x < 0 && facingRight == 1)
        {
            Filp();
        }
    }

    public virtual void Update()
    {
        stateMachine?.CurState?.OnUpdate();
        if (autoFilp)
        {
            AutoFilp();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        curHealth -= damage;
        WorldCanvas.Instacne.ShowMessage(transform.position, Mathf.FloorToInt(damage).ToString());
        if (curHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        MyEventSystem.Instance.EventTrigger<Vector2>("monsDie", transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
        stateMachine?.CurState?.OnExit();
    }
}
