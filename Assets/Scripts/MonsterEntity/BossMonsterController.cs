using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ Phase
/// </summary>
public enum eBossPhase
{
    None,   // ���
    Phase_1,
    Phase_2, // ü���� 70%: ���Ÿ� ����
    Phase_3  // ü���� 30%: ��ų ����
}

/// <summary>
/// ���������� �̵�,���� ���
/// 2025.02.24.ImSeonggyun
/// </summary>
public class BossMonsterController : BaseController
{
    private MonsterManager monsterManager;
    private Transform target;
    private eBossPhase phase;

    // ������ Base�� WeaponPrefab���� �ٰŸ� ���⸦ �Ҵ�
    [SerializeField] public WeaponHandler RangeWeaponPrefab;     // RangeWeaponPrefab���� ���Ÿ� ���⸦ �Ҵ�

    // ���� ������ ���� �ٸ� ����
    [SerializeField] private float followRange;

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterManager = _monsterManager;
        target = _target;

        // ���͸��� ���� �ٸ��Ƿ� �����ÿ� �������ش�
        followRange = _followRange;
        phase = eBossPhase.Phase_1;
    }
    // ���������� �̵�����
    protected override void HandleAction()
    {
        // OOP Ư���� ������ ����� �ߴ� ���� ��������
        base.HandleAction();

        // ���Ͱ� ���� ������ �߰�

        // �Ÿ�,���� ���ϱ�
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        // Phase�� ���� �̵�, ���� ����
        // phase1: �Ϲݸ��Ϳ� �����ϰ� �̵��� ���� �ٰŸ� ����
        // phase2: ���Ÿ� ����
        // phase3: ��ų ����

        // ü�� ������ �ؾ��Ѵ�
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    // ������ ���ݰ� ��ų
    // ��ų�� BaseSkillHandler�� �ʿ��ϸ�
    // �̸� Player�� �������Ͱ� ����� �޾Ƽ� �����Ѵ�
    // Controller�� �װ� �����ͼ� ����


}
