using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    
    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    
    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange =value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }
    
    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    
    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int aniSpeed = Animator.StringToHash("Speed");
    
    public BaseController Controller { get; private set; }
    
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        
        animator.speed = 1.0f / delay; 
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {
        
    }
    
    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
        WaitForIt();
    }
    
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds( 1f / delay);
        animator.ResetTrigger(IsAttack);
    }
    
    public void AttackOutAnimation()
    {
        animator.ResetTrigger(IsAttack);
    }

    public void SetWeaponSpeed(float delay)
    {
        animator.speed = 1f / delay;
        animator.SetFloat(aniSpeed, 1f / delay);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
    
    public void UpgradeDamage(float upgrade)
    {
       power += upgrade;
    }
    
    public void UpgradeDelay(float upgrade)
    {
        // 공격속도 반복주기 감소
        Delay -= upgrade;
        if (Delay < 0)
            Delay = 0.2f;
        SetWeaponSpeed(Delay);
    }
    public void UpgradeBulletSpeed(float upgrade)
    {
        // 투사체 속도
        Speed += upgrade;
    }
    public virtual void UpgradeBulletSize(float upgrade)
    {
       
    }
    public virtual void UpgradeBulletNumber(int upgrade)
    {
        
    }
}
