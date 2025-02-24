using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기들의 Base
/// 플레이어와 몬스터 무기의 Base
/// 2025.02.24.ImSeonggyun
/// </summary>
public class WeaponHandler : MonoBehaviour
{    
    [Header("Attack Info")]
    [SerializeField] private float delay;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power;
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");   // 애니메이션 트리거 만든거 불러온다

    public BaseController Controller { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;

    // 무기에 쓸 사운드 클립 저장
    public AudioClip attackSoundClip;


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();    // 부모한테서 찾는 이유: 무기는 캐릭터의 하위에 생길 것이기 때문
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;  // delay가 느려질수록 애니메이션이 빠르게 작동
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        // 공격 애니메이션 실행
        AttackAnimation();

        // 무기 소리 
        //if (attackSoundClip != null)
        //    SoundManager.PlayClip(attackSoundClip);
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
