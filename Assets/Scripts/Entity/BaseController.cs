using System;
using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    /// <summary>
    /// / 주석 확인 한글 USA 
    /// </summary>
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected AnimationHandler animationHandler;

    internal Status Status;

    [SerializeField] public WeaponHandler WeaponPrefab;
    //protected WeaponHandler weaponHandler;
    [SerializeField] public WeaponHandler weaponHandler;

    protected bool isMonster;           // 애니메이션 flip문제로 캐릭터/몬스터 분할
    protected bool isMoving;
    protected bool isAttacking;
    protected bool isDead;
    private float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            HandleAction();
            Rotate(lookDirection);
            HandleAttackDelay();
        }
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction)
    {
        /// 혹시 direction이 아래를 가리킨다면, statHandler.MaxSpeed가 0인지 살펴볼 것
        direction = direction * Status.speed;   // 몬스터에 MaxSpeed 설정을 안해서 아래로 가고 있었다...
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        isMoving = true;
        if (direction.magnitude < .9f)
        {
            isMoving = false;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    protected virtual void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        if (isMonster)
            characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft); /// 무기 회전
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
    protected virtual void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            animationHandler.Attack(false);
            timeSinceLastAttack += Time.deltaTime;
        }

        if (!isMoving && isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            animationHandler.Attack(true);
            Debug.Log("공격애니메이션 재생");
            Attack();
        }

    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    public virtual void Death()
    {
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            // a값만 바꾼다
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        // gameObject.SetActive(false);
        // Destroy�� ������ Controller���� ����
    }

    public void ChangeWeapon(WeaponHandler weapon)
    {
        Destroy(weaponHandler.gameObject);

        WeaponPrefab = weapon;
        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }
}
