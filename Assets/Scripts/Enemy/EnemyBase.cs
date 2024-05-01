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
    public bool isDie;

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
        if (other.CompareTag("Player") && !isDie)
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (!playerStats.IsInv)
            {
                if (info.energyType == EnergyType.Anode)
                    playerStats.anodeEnergy.AddChange(info.recoverFromAtk);
                else
                    playerStats.cathodeEnergy.AddChange(info.recoverFromAtk);
                playerStats.TakeDamage(info.atkMul);
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
        if (!isDie)
        {
            curHealth -= damage;
            StartDamagedEffect();
            if (curHealth <= 0)
                Die();
        }
    }

    private void StartDamagedEffect()
    {
        StartCoroutine(nameof(DamagedEffect));
    }

    IEnumerator DamagedEffect()
    {
        sr.material.SetFloat("_rate", 1);
        yield return new WaitForSeconds(0.1f);
        sr.material.SetFloat("_rate", 0);
    }

    public virtual void Die(bool e = true)
    {
        isDie = true;
        stateMachine.ChangeState(new DieState(stateMachine, this));
        transform.DOKill();
        if (e)
        {
            MyEventSystem.Instance.EventTrigger<Vector2>("monsDie", transform.position);
        }
        transform.DOScale(0f, 0.5f);
        transform
            .DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    private void OnDestroy()
    {
        transform.DOKill();
        stateMachine?.CurState?.OnExit();
    }
}
