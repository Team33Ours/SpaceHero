using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    private MonsterManager monsterManager;
    private Transform target;
    public eBossPhase phase;

    // 보스는 Base의 WeaponPrefab에는 근거리 무기를 할당
    [SerializeField] public WeaponHandler RangeWeaponPrefab;     // RangeWeaponPrefab에는 원거리 무기를 할당

    // 몬스터 종류에 따라 다른 값들
    [SerializeField] private float followRange;

    private void Awake()
    {
        // 씬이 바뀌는걸 대비해서 프리팹 찾아서 연결해야한다

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
        // phase2: 원거리 공격
        // phase3: 스킬 공격

        
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    // 보스의 공격과 스킬
    // Controller는 그걸 가져와서 쓴다


}
