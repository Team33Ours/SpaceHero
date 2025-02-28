using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using UnityEngine.UIElements;

/// <summary>
/// 보스의 Phase
/// </summary>
public enum eBossPhase
{
    None,   // 사망
    Phase_1,
    Phase_2, // 체력의 70%: 원거리 공격
    Phase_3  // 체력의 30%: 스킬 공격
}

/// <summary>
/// 보스몬스터의 이동,공격 기능
/// 2025.02.24.ImSeonggyun
/// </summary>
public class BossMonsterController : BaseController
{
    //private MonsterManager monsterManager;
    //private Transform target;
    public MonsterManager monsterManager;
    public Transform target;
    public eBossPhase phase;

    public Animator monsterAnimator;    // 몬스터 컨트롤러가 붙은 gameObject의 child에 있는 Animator

    public SpriteRenderer subSprite;    // 자식의 스프라이트(애니메이션에 쓰기 위해 order in layer 조정을 해야한다)


    /// <summary>
    /// 이렇게 하니까 WeaponHandler가 null이다
    /// 그냥 Base에 원거리 무기 주고, 근거리는 취소
    /// </summary>
    [SerializeField] public RangeWeaponHandler RangeWeaponPrefab;     // RangeWeaponPrefab에는 원거리 무기를 할당
    // 일반공격과 스킬은 투사체가 다르다, BaseController에 선언한 BulletIndex로 접근해서 바꿔라
    public ProjectileManager projectileManager;

    // 몬스터 종류에 따라 다른 값들
    [SerializeField] private float followRange;

    // 스킬의 사용
    public GameManager gameManager; // 플레이어를 알고 있다
    public SkillManager skillManager;
    public bool isSkill1OnCooldown;  // 스킬 1이 쿨타임 중
    public bool isSkill2OnCooldown;  // 스킬 2가 쿨타임 중

    public vDv skillAction; // 스킬을 저장할 delegate
    private BaseSkill currentSkill; // 현재 할당된 스킬

    protected override void OnEnable()
    {
        target = GameManager.Instance.playerController.transform;
    }


    private void Start()
    {
        skillManager = SkillManager.Instance;
        projectileManager = ProjectileManager.Instance;
    }

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterAnimator = GetComponentInChildren<Animator>();   // 몬스터의 animator 연결

        subSprite = GetComponentInChildren<SpriteRenderer>();

        monsterManager = _monsterManager;
        target = _target;

        // 몬스터마다 값이 다르므로 생성시에 지정해준다
        followRange = _followRange;
        phase = eBossPhase.Phase_1;
        // 처음에는 모든 기술 쓸 수 있는 상태
        isSkill1OnCooldown = false;
        isSkill2OnCooldown = false;
    }

    /// <summary>
    /// BaseController의 Update를 사용하면 
    /// phase1,3일때에도 HandleAttackDelay를 들어가 원거리 공격을 하게 된다
    /// </summary>
    protected override void Update()
    {
        HandleAction();
        Rotate(lookDirection);  // 회전은 BaseController의 로직을 그대로 따른다
        HandleAttackDelay();    // 일반 공격 or 스킬 실행
    }

    #region 기존의 HandleAction(너무 무겁다)
    // 보스몬스터의 이동, 공격로직
    protected override void HandleAction()
    {
        // OOP 특강때 지우지 말라고 했던 것이 생각났다
        base.HandleAction();

        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        // 거리,방향 구하기
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        // Phase에 따른 이동, 공격 구분
        // phase1: 일반몬스터와 유사하게 일반공격(원거리)
        // phase2: 원거리 스킬 공격
        // phase3: 근거리 스킬 공격
        if (phase == eBossPhase.Phase_1)
        {
            skillAction = null;
            isAttacking = false;  // 공격하고 있지 않다

            // 거리에 따른 판단
            if (distance <= followRange)
            {
                // 이동
                lookDirection = direction;
                // 공격범위에 있다면 
                if (distance <= weaponHandler.AttackRange)
                {
                    // 공격
                    int layerMaskTarget = weaponHandler.target;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                        (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                    // 충돌체가 있다면, 충돌해서 처리해야하는 layer가 맞는지 확인한다
                    // layer가 "Level"의 layer이면 공격하지 않는다
                    if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                    {
                        isAttacking = true;

                        /// 몬스터의 animator 파라미터를 바꾼다
                        monsterAnimator.SetBool("IsAttack", true);
                        Debug.LogFormat($"{monsterAnimator.transform.parent}   + IsAttack + {monsterAnimator.GetBool("IsAttack")}");

                    }
                    movementDirection = Vector2.zero;
                    return;
                }
                movementDirection = direction;
            }
        }
        else if (phase == eBossPhase.Phase_2)
        {
            skillAction = StartSkill1;  /// 방법2.void를 반환하는 메서드 안에서 코루틴을 호출한다
            // 거리에 따른 판단
            //if (distance <= followRange)
            // 거리가 5 이하일 때는 이동하지 않음
            if (distance <= 5f)
            {
                movementDirection = Vector2.zero;  // 10 이하일 때는 이동하지 않음
                lookDirection = direction;
            }
            else
            {
                //skillAction = () => StartCoroutine(UseSkill1()); // 방법1.람다
                skillAction = StartSkill1;
                movementDirection = Vector2.zero;  // 이동 안 함
                lookDirection = direction;
            }
        }
        else if (phase == eBossPhase.Phase_3)
        {
            //skillAction = () => StartCoroutine(UseSkill2()); 
            skillAction = StartSkill2;

            // 스킬 쿨타임 중이라면 이동하지 않음
            if (isSkill2OnCooldown)
            {
                movementDirection = Vector2.zero;  // 쿨타임 중 이동하지 않음
                lookDirection = direction;
                return;
            }
            else
            {
                // 근거리 스킬을 사용할 때는 플레이어를 추격함
                if (distance <= 100f)
                {
                    lookDirection = direction;
                    movementDirection = direction;
                }
            }
        }
    }
    #endregion
    protected override void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        // Phase에 따라 다른 공격 메서드 호출
        if (phase == eBossPhase.Phase_1)
        {
            // 총알 원래대로
            projectileManager.ChangeBossBullet(RangeWeaponPrefab, RangeWeaponPrefab.transform.position, this.LookDirection, 2);
            //base.HandleAttackDelay();   // 일반공격
            base.Attack();
            Debug.Log("일반공격");
        }
        else if (phase == eBossPhase.Phase_2)
        {
            // 총알 교체
            projectileManager.ChangeBossBullet(RangeWeaponPrefab, RangeWeaponPrefab.transform.position, this.LookDirection, 4);

            // 원거리 스킬 공격
            float distance = DistanceToTarget();  // 목표까지의 거리

            if (distance <= 10f) // 일정 거리 이내에서만 스킬 실행
            {
                // 그리고 정지

                if (skillAction != null && !isSkill1OnCooldown)
                    Attack();
            }
            else
            {
                // 건너뛰면 다음프레임에 이동한다
                Debug.Log("대상이 너무 멀어서 스킬1을 실행하지 않습니다.");
            }
        }
        else if (phase == eBossPhase.Phase_3)
        {
            // 근거리 스킬 공격
            float distance = DistanceToTarget();  // 목표까지의 거리
            if (distance >= 1.5f && distance <= 2f) // 특정 거리 이내에서만 스킬 실행
            {
                if (skillAction != null && !isSkill2OnCooldown)
                    Attack();
            }
            else
            {
                // 건너뛰면 다음프레임에 이동한다
                //movementDirection = Vector2.zero;
                Debug.Log("스킬2를 실행하지 않습니다.");
            }
        }
    }
    protected override void Attack()
    {
        skillAction();
    }


    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }


    /// <summary>
    /// 람다를 대신하는 방법
    /// </summary>
    public void StartSkill1()
    {
        StartCoroutine(UseSkill1());
    }
    public void StartSkill2()
    {
        StartCoroutine(UseSkill2());
    }

    // 보스의 공격과 스킬
    // Controller는 그걸 가져와서 쓴다
    public IEnumerator UseSkill1()
    {
        // 현재 쿨타임이 끝나지 않았다면
        if (isSkill1OnCooldown)
            yield break;
        if (phase == eBossPhase.Phase_2)  // skillManager의 bossMobSkills["IceBall"]
            currentSkill = skillManager.bossMobSkills["IceBall"]; // 스킬 이름을 통해 해당 스킬을 할당

        monsterAnimator.SetBool("isUsingSkill1", true);      // 애니메이터 스킬 사용 트리거
        Debug.Log("스킬1: 아이스볼");

        isSkill1OnCooldown = true;  /// 스킬1 쿨타임 시작

        yield return new WaitForSeconds(1);         // 쿨타임 후 시작: 1초
        isSkill1OnCooldown = false; /// 쿨타임 끝

        ResourceController playerResourceController = gameManager.playerController.gameObject.GetComponent<ResourceController>();
        // 스킬 효과 적용: 체력 감소 + 이동 속도 감소 + 일정 시간 후 복구
        StartCoroutine(playerResourceController.TakeDamageAndDebuff(
            currentSkill.value1,  // 데미지
            currentSkill.value2,  // 속도 감소량
            currentSkill.value3   // 지속시간
        ));
        // 스킬 사용 끝났으므로 애니메이터 트리거 false로 설정
        monsterAnimator.SetBool("isUsingSkill1", false);

        // value1: 데미지, value2: 효과, value3: 지속시간
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 데미지: {currentSkill.value1}");
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 이동속도 감소: {currentSkill.value2}");
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 지속시간: {currentSkill.value3}");
    }

    public IEnumerator UseSkill2()
    {
        if (isSkill2OnCooldown)
            yield break;
        if (phase == eBossPhase.Phase_3)    // skillManager의 bossMobSkills["ThunderTackle"]
            currentSkill = skillManager.bossMobSkills["ThunderTackle"]; // 스킬 이름을 통해 해당 스킬을 할당

        monsterAnimator.SetBool("isUsingSkill2", true);  // 애니메이터 스킬 사용 트리거
        subSprite.sortingOrder = 9;
        Debug.Log("스킬2: 썬더태클");

        isSkill2OnCooldown = true;   // 스킬2 쿨타임 시작

        yield return new WaitForSeconds(5);         // 쿨타임 후 시작: 5초
        isSkill2OnCooldown = false; /// 쿨타임 끝
        subSprite.sortingOrder = 0;
        // 플레이어의 ResourceController 가져오기
        ResourceController playerResourceController = gameManager.playerController.GetComponent<ResourceController>();
        // 체력 감소만 적용
        playerResourceController.TakeDamage(currentSkill.value1);

        // 스킬 사용 끝났으므로 애니메이터 트리거 false로 설정
        monsterAnimator.SetBool("isUsingSkill2", false);

        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 데미지: {currentSkill.value1}");
    }
}
