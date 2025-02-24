using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� Base
/// �÷��̾�� ���� ������ Base
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

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");   // �ִϸ��̼� Ʈ���� ����� �ҷ��´�

    public BaseController Controller { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;

    // ���⿡ �� ���� Ŭ�� ����
    public AudioClip attackSoundClip;


    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();    // �θ����׼� ã�� ����: ����� ĳ������ ������ ���� ���̱� ����
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;  // delay�� ���������� �ִϸ��̼��� ������ �۵�
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        // ���� �ִϸ��̼� ����
        AttackAnimation();

        // ���� �Ҹ� 
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
