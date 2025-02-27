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

    public Animator bossAnimator;    // 몬스터 컨트롤러가 붙은 gameObject의 child에 있는 Animator


    /// <summary>
    /// 이렇게 하니까 WeaponHandler가 null이다
    /// 그냥 Base에 원거리 무기 주고, 근거리는 취소
    /// </summary>
    //// 보스는 Base의 WeaponPrefab에는 근거리 무기를 할당
    //[SerializeField] public WeaponHandler RangeWeaponPrefab;     // RangeWeaponPrefab에는 원거리 무기를 할당

    // 몬스터 종류에 따라 다른 값들
    [SerializeField] private float followRange;

    // 스킬의 사용
    public GameManager gameManager; // 플레이어를 알고 있다
    public SkillManager skillManager;
    public bool isSkill2OnCooldown;  // 스킬 2가 쿨타임 중

    public vDv skillAction; // 스킬을 저장할 delegate
    private BaseSkill currentSkill; // 현재 할당된 스킬


    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        skillManager = SkillManager.Instance;
        bossAnimator = GetComponentInChildren<Animator>();   // 몬스터의 animator 연결

        // 보스몬스터만 여기서 생성되니까 구분할 필요 없다
        // 보스몬스터에 붙는 컴포넌트 올바른 자리에 다 붙일것


        monsterManager = _monsterManager;
        target = _target;

        // 몬스터마다 값이 다르므로 생성시에 지정해준다
        followRange = _followRange;
        phase = eBossPhase.Phase_1;
    }
    // 보스몬스터의 이동로직
    protected override void HandleAction()
    {
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
        // phase1: 일반몬스터와 유사하게 이동에 따른 근거리 공격
        // phase2: 원거리 스킬 공격
        // phase3: 근거리 스킬 공격
        if (phase == eBossPhase.Phase_1)
        {
            skillAction = null; // 근접공격이라 별도의 스킬 없음

            // 공격하고 있지 않다
            isAttacking = false;
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
                        bossAnimator.SetBool("IsAttack", true);
                        Debug.LogFormat($"{bossAnimator.transform.parent}   + IsAttack + {bossAnimator.GetBool("IsAttack")}");

                    }
                    /// 이건 0으로 바꿀 필요가 있을까?
                    movementDirection = Vector2.zero;
                    return;
                }
                else
                {
                    // 공격하고 있지 않다
                    isAttacking = false;
                    /// 몬스터의 animator 파라미터를 바꾼다
                    bossAnimator.SetBool("IsAttack", false);
                }
                movementDirection = direction;
            }
        }
        else if (phase == eBossPhase.Phase_2)
        {
            /// 몬스터의 animator 파라미터를 바꾼다
            bossAnimator.SetBool("IsAttack", true);
            Debug.LogFormat($"{bossAnimator.transform.parent}   + IsAttack + {bossAnimator.GetBool("IsAttack")}");

            //skillAction = () => StartCoroutine(UseSkill1()); // 방법1.람다
            skillAction = StartSkill1;  /// 방법2.void를 반환하는 메서드 안에서 코루틴을 호출한다
            movementDirection = Vector2.zero;  // 이동 안 함
        }
        else if (phase == eBossPhase.Phase_3)
        {
            //skillAction = () => StartCoroutine(UseSkill2()); 
            skillAction = StartSkill2;

            // 스킬 쿨타임 중이라면 이동하지 않음
            if (isSkill2OnCooldown)
            {
                // 공격하고 있지 않다
                isAttacking = false;
                /// 몬스터의 animator 파라미터를 바꾼다
                bossAnimator.SetBool("IsAttack", false);

                movementDirection = Vector2.zero;  // 쿨타임 중 이동하지 않음
                return;
            }
            // 근거리 스킬을 사용할 때는 플레이어를 추격함
            if (distance <= followRange)
            {

                lookDirection = direction;
                movementDirection = direction;
            }
        }
    }

    /// <summary>
    /// BaseController의 Update를 사용하면 
    /// phase1,3일때에도 HandleAttackDelay를 들어가 원거리 공격을 하게 된다
    /// </summary>
    protected override void Update()
    {
        HandleAction();         // 자신의 HandleAction
        Rotate(LookDirection);       //

        if (phase == eBossPhase.Phase_2)
        {
            HandleAction(); // 이동을 안한다
        }
        else
        {
            // 이동한다

        }
    }


    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void Attack()
    {
        base.Attack();

        if (skillAction != null)
            skillAction(); // 이제 코루틴이 실행됨
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
        if (currentSkill == null)
            yield break;
        // skillManager의 bossMobSkills["IceBall"]
        if (phase == eBossPhase.Phase_2)
            currentSkill = skillManager.bossMobSkills["IceBall"]; // 스킬 이름을 통해 해당 스킬을 할당

        // 쿨타임 후 시작: 1초
        yield return new WaitForSeconds(1);

        // 플레이어에 직접 접근해서 효과를 주는걸로...
        // 여러개를 리턴하는방법이 익숙하지 않은데 현재 남은 시간상 불가능하다
        ResourceController playerResourceController = gameManager.playerController.gameObject.GetComponent<ResourceController>();
        // 스킬 효과 적용: 체력 감소 + 이동 속도 감소 + 일정 시간 후 복구
        StartCoroutine(playerResourceController.TakeDamageAndDebuff(
            currentSkill.value1,  // 데미지
            currentSkill.value2,  // 속도 감소량
            currentSkill.value3   // 지속시간
        ));

        // value1: 데미지, value2: 효과, value3: 지속시간
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 데미지: {currentSkill.value1}");
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 이동속도 감소: {currentSkill.value2}");
        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 지속시간: {currentSkill.value3}");
    }

    public IEnumerator UseSkill2()
    {
        /// 몬스터의 animator 파라미터를 바꾼다
        bossAnimator.SetBool("IsAttack", true);
        Debug.LogFormat($"{bossAnimator.transform.parent}   + IsAttack + {bossAnimator.GetBool("IsAttack")}");




        if (currentSkill == null)
            yield break;
        // skillManager의 bossMobSkills["ThunderTackle"]
        if (phase == eBossPhase.Phase_3)
            currentSkill = skillManager.bossMobSkills["ThunderTackle"]; // 스킬 이름을 통해 해당 스킬을 할당

        // 쿨타임 중이라면 이동을 멈추고 기다린다
        isSkill2OnCooldown = true;
        // 쿨타임 후 시작: 5초
        yield return new WaitForSeconds(5);


        // 쿨타임 끝난 후
        isSkill2OnCooldown = false;
        // 플레이어의 ResourceController 가져오기
        ResourceController playerResourceController = gameManager.playerController.GetComponent<ResourceController>();
        // 체력 감소만 적용
        playerResourceController.TakeDamage(currentSkill.value1);

        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 데미지: {currentSkill.value1}");

    }
}
