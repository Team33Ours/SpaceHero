using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ϲݸ����� �̵�,���� ���
/// 2025.02.24.ImSeonggyun
/// </summary>
public class NormalMonsterController : BaseController
{
    private MonsterManager monsterManager;
    private Transform target;

    // ���� ������ ���� �ٸ� ����
    [SerializeField] private float followRange;

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterManager = _monsterManager;
        target = _target;

        // ���͸��� ���� �ٸ��Ƿ� �����ÿ� �������ش�
        followRange = _followRange;
    }
    // ����(�Ϲ�)�� �̵�����
    protected override void HandleAction()
    {
        // OOP Ư���� ������ ����� �ߴ� ���� ��������
        base.HandleAction();

        // ���Ͱ� ���� ������ �߰�

        // �Ÿ�,���� ���ϱ�
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        // �����ϰ� ���� �ʴ�

        // �Ÿ��� ���� �Ǵ�
        if (distance <= followRange)
        {
            // �̵�
            lookDirection = direction;
            // ���ݹ����� �ִٸ� 
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
    public override void Death()
    {
        base.Death();
        // ���� �� ������Ʈ Ǯ���� �����Ͽ� List�� ����ִ´�
        monsterManager.RemoveMonsterOnDeath(this);
    }
}
