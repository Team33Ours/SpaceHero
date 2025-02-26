using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

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

    // 보스는 Base의 WeaponPrefab에는 근거리 무기를 할당
    [SerializeField] public WeaponHandler RangeWeaponPrefab;     // RangeWeaponPrefab에는 원거리 무기를 할당

    // 몬스터 종류에 따라 다른 값들
    [SerializeField] private float followRange;

    // 스킬의 사용
    public GameManager gameManager; // 플레이어를 알고 있다
    public SkillManager skillManager;
   

    public vDv skillAction; // 스킬을 저장할 delegate
    private BaseSkill currentSkill; // 현재 할당된 스킬

    private void Awake()
    {
        // 씬이 바뀌는걸 대비해서 프리팹 찾아서 연결해야한다

    }
    private void Start()
    {
        skillManager = SkillManager.Instance;
    }

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterManager = _monsterManager;
        target = _target;

        // 몬스터마다 값이 다르므로 생성시에 지정해준다
        followRange = _followRange;
        phase = eBossPhase.Phase_1;
    }
    // 보스몬스터의 이동로직
    protected override void HandleAction()
    {
        // OOP 특강때 지우지 말라고 했던 것이 생각났다
        base.HandleAction();

        // 몬스터가 무기 장착시 추가

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
        }
        else if (phase == eBossPhase.Phase_2)
        {
            //skillAction = () => StartCoroutine(UseSkill1()); // 방법1.람다
            skillAction = StartSkill1;  /// 방법2.void를 반환하는 메서드 안에서 코루틴을 호출한다
        }
        else if (phase == eBossPhase.Phase_3)
        {
            //skillAction = () => StartCoroutine(UseSkill2()); 
            skillAction = StartSkill2;  
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
        // skillManager의 bossMobSkills["FireBall"]
        // Phase 2일 때 FireBall 스킬을 바로 설정
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
        if (currentSkill == null)
            yield break;
        // skillManager의 bossMobSkills["Thunder Tackle"]
        // Phase 3일 때 Thunder Tackle 스킬을 바로 설정
        if (phase == eBossPhase.Phase_3)
            currentSkill = skillManager.bossMobSkills["Thunder Tackle"]; // 스킬 이름을 통해 해당 스킬을 할당
 
        // 쿨타임 후 시작: 5초
        yield return new WaitForSeconds(5);

        // 플레이어의 ResourceController 가져오기
        ResourceController playerResourceController = gameManager.playerController.GetComponent<ResourceController>();
        // 체력 감소만 적용
        playerResourceController.TakeDamage(currentSkill.value1);

        Debug.Log($"사용한 스킬: {currentSkill.skillName}, 데미지: {currentSkill.value1}");
    }
}
