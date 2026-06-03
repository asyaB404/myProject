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

//    public Vector2 DirectionToPlayer
//    {
//       get
//       {
//          Vector2 d = GameData.CachedPlayerPosition - (Vector2)transform.position;
//          float sq = d.sqrMagnitude;
//          if (sq < 1e-10f)
//             return Vector2.right;
//          float invLen = 1f / Mathf.Sqrt(sq);
//          return d * invLen;
//       }
//    }

    Vector2 _toPlayer;
    float _sqrDistToPlayer;
    Vector2 _directionToPlayer;

    /// <summary>本帧 Update 内已刷新；指向玩家的单位向量（每怪每帧至多一次 Sqrt）。</summary>
    public Vector2 DirectionToPlayer => _directionToPlayer;

    /// <summary>本帧内指向玩家的未归一化偏移。</summary>
    public Vector2 ToPlayer => _toPlayer;

    /// <summary>本帧内与玩家距离的平方，用于射程比较，避免重复 Sqrt。</summary>
    public float SqrDistanceToPlayer => _sqrDistToPlayer;

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

    static readonly int RateId = Shader.PropertyToID("_rate");
    const float DamageFlashDuration = 0.1f;

    MaterialPropertyBlock _flashMpb;
    float flashUntil;

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
        _flashMpb = new MaterialPropertyBlock();
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
        float dx = _toPlayer.x;
        if (dx > 0 && facingRight == -1)
            Filp();
        else if (dx < 0 && facingRight == 1)
            Filp();
    }

    void RefreshPlayerTracking()
    {
        _toPlayer = GameData.CachedPlayerPosition - (Vector2)transform.position;
        _sqrDistToPlayer = _toPlayer.sqrMagnitude;
        if (_sqrDistToPlayer < 1e-10f)
            _directionToPlayer = Vector2.right;
        else
            _directionToPlayer = _toPlayer * (1f / Mathf.Sqrt(_sqrDistToPlayer));
    }

    /// <summary>按当前位置即时计算方向（如 DOTween 延迟开火），不走帧缓存。</summary>
    public Vector2 GetDirectionToPlayerNow()
    {
        Vector2 d = GameData.CachedPlayerPosition - (Vector2)transform.position;
        float sq = d.sqrMagnitude;
        if (sq < 1e-10f)
            return Vector2.right;
        return d * (1f / Mathf.Sqrt(sq));
    }

    public virtual void Update()
    {
        RefreshPlayerTracking();
        stateMachine?.CurState?.OnUpdate();
        if (autoFilp)
        {
            AutoFilp();
        }
    }

    void LateUpdate()
    {
        if (flashUntil > 0f && Time.time >= flashUntil)
            EndDamageFlash();
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
        flashUntil = Time.time + DamageFlashDuration;
        _flashMpb.SetFloat(RateId, 1f);
        sr.SetPropertyBlock(_flashMpb);
    }

    void EndDamageFlash()
    {
        flashUntil = 0f;
        _flashMpb.SetFloat(RateId, 0f);
        sr.SetPropertyBlock(_flashMpb);
    }

    public virtual void Die(bool e = true)
    {
        isDie = true;
        touchingPlayer = false;
        if (flashUntil > 0f)
            EndDamageFlash();
        stateMachine.ChangeState(new DieState(stateMachine, this));
        transform.DOKill();
        if (e)
        {
            MyEventSystem.Instance.EventTrigger<EnemyBase>(GameEventType.MonsDie, this);
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
