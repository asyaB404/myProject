using System.Collections;
using DG.Tweening;
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
            Vector2 d = GameData.CachedPlayerPosition - (Vector2)transform.position;
            float sq = d.sqrMagnitude;
            if (sq < 1e-10f)
                return Vector2.right;
            float invLen = 1f / Mathf.Sqrt(sq);
            return d * invLen;
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

    /// <summary>每只怪生成时在配置 speed 基础上的倍率 ∈ [0.9, 1.1]，让同屏集群速度略有差异。</summary>
    float instanceSpeedFactor = 1f;

    /// <summary><see cref="EnemyInfo.speed"/> × 实例随机倍率。</summary>
    public float EffectiveMoveSpeed => info != null ? info.speed * instanceSpeedFactor : 0f;

    static readonly MaterialPropertyBlock FlashMpb = new MaterialPropertyBlock();
    Coroutine damageFlashCo;

    bool touchingPlayer;
    float nextContactSoundTime;

    public virtual void Start()
    {
        curHealth = info.Health;
        instanceSpeedFactor = 1f + MyRandom.Instance.NextFloat(-0.1f, 0.1f);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = new EnemyStateMachine();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            touchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            touchingPlayer = false;
    }

    /// <summary>替代 OnTriggerStay：由 FixedUpdate 统一处理贴身回能与伤害；音效节流降低尖峰。</summary>
    private void FixedUpdate()
    {
        if (!touchingPlayer || isDie || PlayerController.Instance == null)
            return;
        PlayerStats ps = PlayerController.Instance.playerStats;
        if (ps.IsInv)
            return;

        if (info.energyType == EnergyType.Anode)
        {
            ps.AnodeEnergy += info.RecoverFromAtk;
            if (Time.time >= nextContactSoundTime)
            {
                MusicMgr.Instance.PlaySound("atk_yang");
                nextContactSoundTime = Time.time + 0.12f;
            }
        }
        else
        {
            ps.CathodeEnergy += info.RecoverFromAtk;
            if (Time.time >= nextContactSoundTime)
            {
                MusicMgr.Instance.PlaySound("atk_yin");
                nextContactSoundTime = Time.time + 0.12f;
            }
        }

        ps.TakeDamage(info.AtkMul);
    }

    public void Filp()
    {
        facingRight = -facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void AutoFilp()
    {
        float dx = GameData.CachedPlayerPosition.x - transform.position.x;
        if (dx > 0 && facingRight == -1)
            Filp();
        else if (dx < 0 && facingRight == 1)
            Filp();
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
        if (damageFlashCo != null)
            StopCoroutine(damageFlashCo);
        damageFlashCo = StartCoroutine(DamagedEffect());
    }

    IEnumerator DamagedEffect()
    {
        FlashMpb.SetFloat("_rate", 1f);
        sr.SetPropertyBlock(FlashMpb);
        yield return new WaitForSeconds(0.1f);
        FlashMpb.SetFloat("_rate", 0f);
        sr.SetPropertyBlock(FlashMpb);
        damageFlashCo = null;
    }

    public virtual void Die(bool e = true)
    {
        isDie = true;
        touchingPlayer = false;
        stateMachine.ChangeState(new DieState(stateMachine, this));
        transform.DOKill();
        if (e)
        {
            MyEventSystem.Instance.EventTrigger<EnemyBase>("monsDie", this);
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
        if (WorldCanvas.Instacne != null)
            WorldCanvas.Instacne.UnregisterEnemyFloatThrottle(GetInstanceID());
        transform.DOKill();
        stateMachine?.CurState?.OnExit();
    }
}
